using FlightInvoice.SftpReader.Service.IService;
using FlightInvoice.SftpReader.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using WinSCP;
using System.Reflection;
using FlightInvoice.SftpReader.Utility;
using FlightInvoice.SftpReader.Models;
using FlightInvoice.SftpReader.Models.Dto;
using Newtonsoft.Json;

namespace FlightInvoice.SftpReader
{
    public class SftpFileChecker
    {
        private readonly IServiceProvider _serviceProvider = BuildSerives();

        private static IServiceProvider BuildSerives()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddOptions();

            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddHttpClient();
            serviceCollection.AddHttpClient<ISftpFileService, SftpFileService>();
            serviceCollection.AddScoped<IBaseService, BaseService>();
            serviceCollection.AddScoped<ISftpFileService, SftpFileService>();
            serviceCollection.AddSingleton<SftpFileService>();

            var configuration = BuildConfig();

            SD.SftpFileApiBase = configuration.GetValue<string>("ServiceUrls:SftpFileApi");
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            var provider = serviceCollection.BuildServiceProvider();
            return provider;
        }
        private static IConfigurationRoot BuildConfig()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var builder = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("sftpreader.appsettings.json");
            return builder.Build();
        }

        public List<Byte[]> Streams { get; set; } = new List<Byte[]>();

        public SftpFileChecker(string host, string username, string password, int port, string fingerPrint, string path)
        {
            var provider = _serviceProvider.GetService<SftpFileService>();
            List<SftpFileDto>? list = new();
            ResponseDto? response = provider.GetSftpFilesAsync().Result;

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<SftpFileDto>>(Convert.ToString(response.Result));
            }

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = host,
                UserName = username,
                Password = password,
                PortNumber = port,
                SshHostKeyFingerprint = fingerPrint
            };

            DataTable files = new DataTable("SftpFile");
            files.Columns.Add("Path", typeof(string));
            files.Columns.Add("Date", typeof(DateTime));
            files.Columns.Add("Size", typeof(long));
            files.PrimaryKey = new DataColumn[] { files.Columns["Path"] };

            list.ForEach((item) =>
            {
                files.Rows.Add(item.Path, item.Date, item.Size);
            });

            using (Session session = new Session())
            {
                session.Open(sessionOptions);

                var directoryInfo = session.ListDirectory(path);

                foreach (RemoteFileInfo directory in directoryInfo.Files)
                {
                    if (!directory.IsDirectory)
                        continue;

                    if (directory.IsParentDirectory)
                        continue;

                    var fileInfo = session.ListDirectory(directory.FullName);

                    foreach (RemoteFileInfo file in fileInfo.Files)
                    {
                        if (file.IsDirectory)
                            continue;

                        if (file.IsParentDirectory)
                            continue;

                        string fullname = file.FullName;
                        DataRow row = files.Rows.Find(fullname);

                        if (row == null)
                        {
                            row = files.NewRow();
                            row["Path"] = fullname;
                            files.Rows.Add(row);
                        }

                        bool import = !Object.Equals(row["Date"], file.LastWriteTime) || !Object.Equals(row["Size"], file.Length);

                        row["Date"] = file.LastWriteTime;
                        row["Size"] = file.Length;

                        TransferOptions transferOptions = new TransferOptions();
                        transferOptions.TransferMode = TransferMode.Binary;

                        if (import)
                        {
                            var published = provider.CreateSftpFileAsync(new()
                            {
                                Date = file.LastWriteTime,
                                Path = fullname,
                                Size = file.Length
                            }).Result;

                            if (published != null && published.IsSuccess)
                            {
                                using (var stream = session.GetFile(fullname, transferOptions))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        int read = 0;
                                        byte[] buffer = new byte[1024 * 4];

                                        while ((read = stream.Read(buffer, 0, buffer.Length)) != 0)
                                            ms.Write(buffer, 0, read);
                                        Streams.Add(ms.ToArray());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

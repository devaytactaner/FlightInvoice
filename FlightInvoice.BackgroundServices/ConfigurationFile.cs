using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInvoice.BackgroundServices
{
    public class ConfigurationFile
    {
        public SftpOptions Sftp { get; } = new SftpOptions();
    }
    public class SftpOptions
    {
        public DusseldorfOptions Dusseldorf { get; } = new DusseldorfOptions();

    }
    public class DusseldorfOptions
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string FingerPrint { get; set; }
        public string Path { get; set; }
        public string DeveloperMail { get; set; }
        public string CustomerMail { get; set; }
    }

}

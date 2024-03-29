using FlightInvoice.BackgroundServices.Models;
using FlightInvoice.BackgroundServices.Models.Dto;
using FlightInvoice.BackgroundServices.Service;
using FlightInvoice.BackgroundServices.Service.IService;
using FlightInvoice.SftpReader;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace FlightInvoice.BackgroundServices;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private static ConfigurationFile _configuration;
    private readonly IFlightService _flightService;
    private Queue<MailMessage> _emailQueue;
    private DateTime _nextSyncTime;
    private PrivateLogFormatter _formatter;
    public Worker(ILogger<Worker> logger, ConfigurationFile configuration, IFlightService flightService)
    {
        _logger = logger;
        _configuration = configuration;
        _flightService = flightService;
        _emailQueue = new Queue<MailMessage>();
        _formatter = new PrivateLogFormatter();
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning($"FlightInvoice.BackgroundServicesListener {nameof(Worker)} started");

        try
        {
            bool busy;

            do
            {
                busy = true;

                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    SftpFileChecker checker = new SftpFileChecker(_configuration.Sftp.Dusseldorf.Host, _configuration.Sftp.Dusseldorf.Username, _configuration.Sftp.Dusseldorf.Password, _configuration.Sftp.Dusseldorf.Port, _configuration.Sftp.Dusseldorf.FingerPrint, _configuration.Sftp.Dusseldorf.Path);

                    checker.Streams.ForEach(async (item) =>
                    {
                        var pdfConverter = new PdfConverter.PdfToDataTable(new MemoryStream(item));
                        DataSet set = pdfConverter.ConvertDusseldorfInvoice();

                        DataTable invoiceInfo = set.Tables[0];
                        DataTable invoiceDetail = set.Tables[1];

                        int? invoiceNo = invoiceInfo.Rows[0]["Nummer"] is DBNull ? null : int.Parse((string)invoiceInfo.Rows[0]["Nummer"]);

                        if (!invoiceNo.HasValue)
                            throw new Exception("Invoice Error. Check Invoice Info");

                        int totalRecord = 0;
                        int totalSuccessRecord = 0;
                        double totalPrice = 0;

                        StringBuilder sb = new();

                        foreach (DataRow row in invoiceDetail.Rows)
                        {
                            totalRecord++;

                            string carrierCode = row["CarrierCode"] is DBNull ? string.Empty : (string)row["CarrierCode"];
                            int? flightnumber = row["Flug Nr"] is DBNull ? null : int.Parse((string)row["Flug Nr"]);
                            int? anzahl = row["Anzahl"] is DBNull ? null : ((string)row["Anzahl"]).Contains("-") ? -1 : int.Parse((string)row["Anzahl"]);
                            DateTime? flugdatum = row["Flugdatum"] is DBNull ? null : DateTime.ParseExact((string)row["Flugdatum"], "dd.MM.yyyy", CultureInfo.InvariantCulture);

                            if (string.IsNullOrEmpty(carrierCode) || !flightnumber.HasValue || !flugdatum.HasValue)
                                throw new Exception("Invoice Error. Check Invoice Detail");

                            if (anzahl.HasValue && anzahl.Value == -1)
                            {
                                sb.AppendLine($"CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record not has minus value reservesion table.");
                                continue;
                            }

                            double? einzelpreis = row["Einzelpreis"] is DBNull ? null : double.Parse((string)row["Einzelpreis"]);

                            if (!einzelpreis.HasValue)
                                continue;

                            double total = anzahl!.Value * einzelpreis.Value;

                            ResponseDto? response = await _flightService.GetFlightAsync(carrierCode, flightnumber.Value, flugdatum.Value);

                            if (response != null && response.IsSuccess)
                            {
                                var flightDto = JsonConvert.DeserializeObject<List<FlightDto>>(Convert.ToString(response.Result));

                                if (flightDto == null || flightDto.Count == 0)
                                {
                                    sb.AppendLine($"Exception => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record not match in reservesion table.");
                                }
                                else
                                {
                                    int flightDtoTotalCount = flightDto!.Count;
                                    int hasInvoiceNumber = flightDto.Where(f => !(f.InvoiceNumber is null && f.Price == einzelpreis.Value)).Count();
                                    int noInvoiceNumber = flightDto.Where(f => (f.InvoiceNumber is null && f.Price == einzelpreis.Value)).Count();

                                    if (flightDtoTotalCount == hasInvoiceNumber)
                                    {
                                        sb.AppendLine($"Exception => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. There are no tickets available for sale.");
                                    }
                                    else if (noInvoiceNumber > anzahl)
                                    {
                                        var hasSamePrices = flightDto.Where(f => f.InvoiceNumber is null && f.Price == einzelpreis.Value).Count();

                                        if (hasSamePrices == 0)
                                        {
                                            sb.AppendLine($"Exception => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record price not match reservesion table.");
                                        }
                                        else if (hasSamePrices > anzahl)
                                        {
                                            sb.AppendLine($"Successed => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record has multiple prices. Despite this, there are records with the same invoice price.");
                                            for (int i = 0; i < anzahl; i++)
                                                await _flightService.UpdateFlightAsync(carrierCode, flightnumber.Value, flugdatum.Value.ToString("dd.MM.yyyy"), einzelpreis.Value, invoiceNo.Value);

                                            totalSuccessRecord++;
                                            totalPrice += total;
                                        }
                                        else if (hasSamePrices < anzahl)
                                        {
                                            sb.AppendLine($"Exception =>CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. There are more records in the invoice than in the reservation table.");
                                        }
                                        else
                                        {
                                            sb.AppendLine($"Successed => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record updated.");

                                            for (int i = 0; i < anzahl; i++)
                                                await _flightService.UpdateFlightAsync(carrierCode, flightnumber.Value, flugdatum.Value.ToString("dd.MM.yyyy"), einzelpreis.Value, invoiceNo.Value);

                                            totalSuccessRecord++;
                                            totalPrice += total;
                                        }
                                    }
                                    else if (noInvoiceNumber < anzahl)
                                    {
                                        sb.AppendLine($"Exception =>CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. There are more records in the invoice than in the reservation table.");
                                    }
                                    else if (noInvoiceNumber == anzahl)
                                    {
                                        var prices = flightDto.Where(f => f.InvoiceNumber is null && f.Price == einzelpreis.Value).Select(f => f.Price).Distinct().ToArray();

                                        if (prices.Length > 1)
                                        {
                                            sb.AppendLine($"Exception =>CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record price not match reservesion table.");
                                        }
                                        else
                                        {
                                            sb.AppendLine($"Successed => CarrierCode: {carrierCode}{flightnumber.Value}, FlightDate: {flugdatum.Value.ToString("dd.MM.yyyy")}. This record updated.");

                                            for (int i = 0; i < anzahl; i++)
                                                await _flightService.UpdateFlightAsync(carrierCode, flightnumber.Value, flugdatum.Value.ToString("dd.MM.yyyy"), einzelpreis.Value, invoiceNo.Value);

                                            totalSuccessRecord++;
                                            totalPrice += total;
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Unexpected Error:x1");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Unexpected Error:x2");
                            }
                        }

                        int totalExceptionRecord = totalRecord - totalSuccessRecord;

                        sb.AppendLine($"There are {totalRecord} records in the invoice, {totalSuccessRecord} successful, {totalExceptionRecord} exception records");

                        if (sb.Length > 0)
                            SendQueuedEmails(sb);
                    });

                    if (busy)
                        _nextSyncTime = DateTime.Now;

                    SendQueuedEmails();
                }
                catch (ThreadAbortException e)
                {
                    _logger.LogError(e, "RowId:155");
                    HandleError(LogDetails("Exception occured", new { Exception = e }));
                    await Task.CompletedTask;
                }
                catch (OperationCanceledException e)
                {
                    _logger.LogError(e, "RowId:206");
                    break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "RowId:211");
                    HandleError(LogDetails("Exception occured", new { Exception = e }));
                }
                finally
                {
                    busy = false;
                }
            } while (!cancellationToken.WaitHandle.WaitOne(busy ? 15000 : 0));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "RowId:222");
            throw;
        }

        await Task.CompletedTask;
    }

    public string LogDetails(string message, object detailsObject)
    {
        return String.Format("{0} ({1})", message, String.Join(", ", detailsObject.GetType().GetProperties().Select(p => String.Format("{0}: {1}", p.Name, String.Format(_formatter, "{0}", p.GetValue(detailsObject, null)))).ToArray()));
    }

    private void HandleError(string details)
    {
        MailMessage msg = new MailMessage();
        msg.Subject = "FlightInvoice.BackgroundServicesListener Service Error";
        _configuration.Sftp.Dusseldorf.DeveloperMail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(m => msg.To.Add(m));
        msg.Body = details;

        if (msg.Body.ToLowerInvariant().Contains("timeout expired") && msg.Body.ToLowerInvariant().Contains("the operation has timed out"))
            return;

        QueueEmail(msg);
    }

    public void QueueEmail(MailMessage msg)
    {
        _emailQueue.Enqueue(msg);
    }

    private void SendQueuedEmails(StringBuilder sb)
    {
        MailMessage mail = new MailMessage();

        try
        {
            _configuration.Sftp.Dusseldorf.CustomerMail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(m => mail.To.Add(m));
            mail.Subject = "FlightInvoice.BackgroundServicesListener New Invoice";
            mail.Body = "FlightInvoice.BackgroundServicesListener Record Info";
            mail.IsBodyHtml = true;
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString())))
            {
                Attachment attachment = new Attachment(stream, new ContentType("text/csv"));
                attachment.Name = "invoice info.csv";
                mail.Attachments.Add(attachment);
                string mailAddress = string.Empty;
                SmtpClient client = GetSmtpClient(ref mailAddress);
                mail.From = new MailAddress(mailAddress);                
                client.Send(mail);
            }
        }
        catch(Exception e)
        {
            
        }
    }

    private void SendQueuedEmails()
    {
        if (_emailQueue.Count == 0)
            return;

        while (_emailQueue.Count > 0)
        {
            MailMessage msg = _emailQueue.Dequeue();

            try
            {
                string mailAddress = string.Empty;
                SmtpClient client = GetSmtpClient(ref mailAddress);
                msg.From = new MailAddress(mailAddress);
                client.Send(msg);
            }
            catch
            {
                _emailQueue.Enqueue(msg);
            }
        }
    }

    private static SmtpClient GetSmtpClient(ref string mailAddress)
    {
        SmtpClient smtp = new SmtpClient();

        smtp.Host = "smtp.dogrumail.com";
        smtp.Port = 587;
        smtp.EnableSsl = false;

        Random random = new Random();
        int n = random.Next(0, 2);

        switch (n)
        {
            case 0:
                smtp.Credentials = new NetworkCredential("mailaddress", "password");
                mailAddress = "mailaddress";
                break;
            case 1:
                smtp.Credentials = new NetworkCredential("mailaddress", "password");
                mailAddress = "mailaddress";
                break;
            default:
                throw new Exception("Unexpected error (50)");
        }

        return smtp;
    }
}


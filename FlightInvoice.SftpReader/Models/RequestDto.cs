using static FlightInvoice.SftpReader.Utility.SD;

namespace FlightInvoice.SftpReader.Models;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
}

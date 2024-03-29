using static FlightInvoice.BackgroundServices.Utility.SD;

namespace FlightInvoice.BackgroundServices.Models;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
}

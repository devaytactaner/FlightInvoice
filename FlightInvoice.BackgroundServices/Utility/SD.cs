namespace FlightInvoice.BackgroundServices.Utility;

public class SD
{
    public static string FlightApiBase {  get; set; }
    public enum ApiType
    {
        GET,
        POST, 
        PUT,
        DELETE
    }
}

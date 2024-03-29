using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.SftpReader.Models.Dto;

public class SftpFileDto
{
    public string Path { get; set; }
    public DateTime Date { get; set; }
    public long Size { get; set; }
}

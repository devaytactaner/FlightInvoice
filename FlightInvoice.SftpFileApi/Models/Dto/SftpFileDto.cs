using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.SftpFileApi.Models.Dto;

public class SftpFileDto
{
    public string Path { get; set; }
    public DateTime Date { get; set; }
    public long Size { get; set; }
}

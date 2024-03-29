using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.SftpFileApi.Models
{
    [PrimaryKey(nameof(Path))]
    public class SftpFile
    {
        public string Path { get; set; }

        [Required]
        public DateTime Date { get; set; }
                
        [Required]
        public long Size{ get; set; }        
    }
}

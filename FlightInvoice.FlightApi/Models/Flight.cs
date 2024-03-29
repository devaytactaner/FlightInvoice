using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightInvoice.FlightApi.Models
{
    [PrimaryKey(nameof(BookingId), nameof(Customer), nameof(CarrierCode), nameof(FlightNo), nameof(FlightDate))]
    public class Flight
    {
        public int BookingId { get; set; }

        public string  Customer { get; set; }

        public string CarrierCode { get; set; } 
        
        public int FlightNo { get; set; }  
        
        public DateTime FlightDate { get; set; }

        [Required]
        public string Origin { get; set; }    
        
        [Required]         
        public string Destination { get; set; }

        [Required]
        public double Price { get; set; }

        public int? InvoiceNumber { get; set; }

    }
}

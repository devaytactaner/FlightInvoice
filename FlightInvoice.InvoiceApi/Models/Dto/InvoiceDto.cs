using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.InvoiceApi.Models.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime FlightDate { get; set; }

        public string FlightCode { get; set; }

        public int FlightNumber { get; set; }

        public int SoldSeatCount { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }
    }
}

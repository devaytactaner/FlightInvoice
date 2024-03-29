using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.InvoiceApi.Models;

[PrimaryKey(nameof(Id), nameof(Date), nameof(FlightDate), nameof(FlightCode), nameof(FlightNumber))]
public class Invoice
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    [Required]
    public DateTime FlightDate { get; set; }

    [Required]
    public string FlightCode { get; set; }

    [Required]
    public int FlightNumber { get; set; }

    [Required]
    public int SoldSeatCount { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public string Currency { get; set; }
}

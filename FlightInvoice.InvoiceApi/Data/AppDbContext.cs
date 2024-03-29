using FlightInvoice.InvoiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightInvoice.InvoiceApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Invoice> Invoice { get; set; }
}
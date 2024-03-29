using FlightInvoice.FlightApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightInvoice.FlightApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Flight> Flight { get; set; }
            
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 160, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "BER", Price = 66, Currency = "EUR" });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 160, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "BER", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("10.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("10.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 180, FlightDate = DateTime.Parse("9.01.2024"), Origin = "AYT", Destination = "DUS", Price = 176, Currency = "EUR", InvoiceNumber = 10406 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 180, FlightDate = DateTime.Parse("9.01.2024"), Origin = "AYT", Destination = "DUS", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("10.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("10.01.2024"), Origin = "AYT", Destination = "FRA", Price = 186, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("10.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 143, FlightDate = DateTime.Parse("4.01.2024"), Origin = "FRA", Destination = "AYT", Price = 76, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 143, FlightDate = DateTime.Parse("4.01.2024"), Origin = "FRA", Destination = "AYT", Price = 76, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 143, FlightDate = DateTime.Parse("4.01.2024"), Origin = "FRA", Destination = "AYT", Price = 76, Currency = "EUR", InvoiceNumber = 10406 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 143, FlightDate = DateTime.Parse("4.01.2024"), Origin = "FRA", Destination = "AYT", Price = 76, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 232, FlightDate = DateTime.Parse("13.01.2024"), Origin = "AYT", Destination = "HAJ", Price = 156, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 232, FlightDate = DateTime.Parse("13.01.2024"), Origin = "AYT", Destination = "HAJ", Price = 156, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 145, FlightDate = DateTime.Parse("10.01.2024"), Origin = "FRA", Destination = "AYT", Price = 56, Currency = "EUR", InvoiceNumber = 10406 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 233, FlightDate = DateTime.Parse("13.01.2024"), Origin = "HAJ", Destination = "AYT", Price = 56, Currency = "EUR", InvoiceNumber = 10406 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 233, FlightDate = DateTime.Parse("13.01.2024"), Origin = "HAJ", Destination = "AYT", Price = 56, Currency = "EUR", InvoiceNumber = 10406 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 231, FlightDate = DateTime.Parse("4.01.2024"), Origin = "HAJ", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 231, FlightDate = DateTime.Parse("4.01.2024"), Origin = "HAJ", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 184, FlightDate = DateTime.Parse("9.01.2024"), Origin = "AYT", Destination = "DUS", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 184, FlightDate = DateTime.Parse("9.01.2024"), Origin = "AYT", Destination = "DUS", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 171, FlightDate = DateTime.Parse("9.01.2024"), Origin = "HAM", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 171, FlightDate = DateTime.Parse("9.01.2024"), Origin = "HAM", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 111, FlightDate = DateTime.Parse("9.01.2024"), Origin = "BSL", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 111, FlightDate = DateTime.Parse("9.01.2024"), Origin = "BSL", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "CGN", Price = 110, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "CGN", Price = 110, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "CGN", Price = 110, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "CGN", Price = 110, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 120, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "ZRH", Price = 106, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 120, FlightDate = DateTime.Parse("14.01.2024"), Origin = "AYT", Destination = "ZRH", Price = 106, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 127, FlightDate = DateTime.Parse("14.01.2024"), Origin = "SCN", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 127, FlightDate = DateTime.Parse("14.01.2024"), Origin = "SCN", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 167, FlightDate = DateTime.Parse("8.01.2024"), Origin = "NUE", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 167, FlightDate = DateTime.Parse("8.01.2024"), Origin = "NUE", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 167, FlightDate = DateTime.Parse("8.01.2024"), Origin = "NUE", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "CGN", Price = 126, Currency = "EUR", InvoiceNumber = 10405 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 114, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "CGN", Price = 126, Currency = "EUR", InvoiceNumber = 10405 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 169, FlightDate = DateTime.Parse("13.01.2024"), Origin = "NUE", Destination = "AYT", Price = 56, Currency = "EUR", InvoiceNumber = 10405 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("12.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("12.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 130, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "MUC", Price = 126, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 140, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "FRA", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("11.01.2024"), Origin = "DUS", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("11.01.2024"), Origin = "DUS", Destination = "AYT", Price = 46, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("11.01.2024"), Origin = "DUS", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("11.01.2024"), Origin = "DUS", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("10.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 76, Currency = "EUR", InvoiceNumber = 10404 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 121, FlightDate = DateTime.Parse("10.01.2024"), Origin = "ZRH", Destination = "AYT", Price = 76, Currency = "EUR", InvoiceNumber = 10404 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 168, FlightDate = DateTime.Parse("13.01.2024"), Origin = "AYT", Destination = "NUE", Price = 30, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 168, FlightDate = DateTime.Parse("13.01.2024"), Origin = "AYT", Destination = "NUE", Price = 76, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 168, FlightDate = DateTime.Parse("13.01.2024"), Origin = "AYT", Destination = "NUE", Price = 76, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 160, FlightDate = DateTime.Parse("11.01.2024"), Origin = "AYT", Destination = "BER", Price = 96, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 160, FlightDate = DateTime.Parse("11.01.2024"), Origin = "AYT", Destination = "BER", Price = 96, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 160, FlightDate = DateTime.Parse("11.01.2024"), Origin = "AYT", Destination = "BER", Price = 96, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 151, FlightDate = DateTime.Parse("12.01.2024"), Origin = "STR", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 151, FlightDate = DateTime.Parse("12.01.2024"), Origin = "STR", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 150, FlightDate = DateTime.Parse("11.01.2024"), Origin = "AYT", Destination = "STR", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 150, FlightDate = DateTime.Parse("11.01.2024"), Origin = "AYT", Destination = "STR", Price = 176, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 133, FlightDate = DateTime.Parse("7.01.2024"), Origin = "MUC", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 133, FlightDate = DateTime.Parse("7.01.2024"), Origin = "MUC", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 130, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "MUC", Price = 116, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 130, FlightDate = DateTime.Parse("12.01.2024"), Origin = "AYT", Destination = "MUC", Price = 116, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 185, FlightDate = DateTime.Parse("13.01.2024"), Origin = "DUS", Destination = "AYT", Price = 76, Currency = "EUR", InvoiceNumber = 10403 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 185, FlightDate = DateTime.Parse("13.01.2024"), Origin = "DUS", Destination = "AYT", Price = 76, Currency = "EUR", InvoiceNumber = 10403 });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 191, FlightDate = DateTime.Parse("13.01.2024"), Origin = "VIE", Destination = "AYT", Price = 80, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 191, FlightDate = DateTime.Parse("13.01.2024"), Origin = "VIE", Destination = "AYT", Price = 80, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 191, FlightDate = DateTime.Parse("13.01.2024"), Origin = "VIE", Destination = "AYT", Price = 80, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 151, FlightDate = DateTime.Parse("6.01.2024"), Origin = "STR", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 151, FlightDate = DateTime.Parse("6.01.2024"), Origin = "STR", Destination = "AYT", Price = 56, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("1.01.2024"), Origin = "DUS", Destination = "AYT", Price = 66, Currency = "EUR", });
    //    modelBuilder.Entity<Flight>().HasData(new Flight { CarrierCode = "XQ", FlightNo = 181, FlightDate = DateTime.Parse("1.01.2024"), Origin = "DUS", Destination = "AYT", Price = 66, Currency = "EUR", });

    //}
}

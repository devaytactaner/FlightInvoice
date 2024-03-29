using FlightInvoice.SftpFileApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightInvoice.SftpFileApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<SftpFile> SftpFile { get; set; }
}
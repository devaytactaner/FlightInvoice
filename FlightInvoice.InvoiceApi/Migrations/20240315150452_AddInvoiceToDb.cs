using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightInvoice.InvoiceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlightNumber = table.Column<int>(type: "int", nullable: false),
                    SoldSeatCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => new { x.Id, x.Date, x.FlightDate, x.FlightCode, x.FlightNumber });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}

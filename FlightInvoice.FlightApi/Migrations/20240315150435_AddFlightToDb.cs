using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightInvoice.FlightApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFlightToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarrierCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlightNo = table.Column<int>(type: "int", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => new { x.BookingId, x.Customer, x.CarrierCode, x.FlightNo, x.FlightDate });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}

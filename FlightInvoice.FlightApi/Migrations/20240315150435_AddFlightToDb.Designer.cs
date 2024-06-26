﻿// <auto-generated />
using System;
using FlightInvoice.FlightApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightInvoice.FlightApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240315150435_AddFlightToDb")]
    partial class AddFlightToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightInvoice.FlightApi.Models.Flight", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarrierCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FlightNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoiceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("BookingId", "Customer", "CarrierCode", "FlightNo", "FlightDate");

                    b.ToTable("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}

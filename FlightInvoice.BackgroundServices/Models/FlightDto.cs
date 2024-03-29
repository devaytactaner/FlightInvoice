﻿using System.ComponentModel.DataAnnotations;

namespace FlightInvoice.BackgroundServices.Models.Dto;

public class FlightDto
{
    public int BookingId { get; set; }

    public string Customer { get; set; }

    public string CarrierCode { get; set; }

    public int FlightNo { get; set; }

    public string FlightDate { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public double Price { get; set; }

    public int? InvoiceNumber { get; set; }
}

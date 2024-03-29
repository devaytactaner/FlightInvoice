using AutoMapper;
using FlightInvoice.InvoiceApi.Models;
using FlightInvoice.InvoiceApi.Models.Dto;

namespace FlightInvoice.InvoiceApi;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<InvoiceDto, Invoice>();
            config.CreateMap<Invoice, InvoiceDto>();
        });

        return mappingConfig;
    }
}

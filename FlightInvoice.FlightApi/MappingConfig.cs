using AutoMapper;
using FlightInvoice.FlightApi.Models;
using FlightInvoice.FlightApi.Models.Dto;

namespace FlightInvoice.FlightApi;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<FlightDto, Flight>();
            config.CreateMap<Flight, FlightDto>();
        });

        return mappingConfig;
    }
}

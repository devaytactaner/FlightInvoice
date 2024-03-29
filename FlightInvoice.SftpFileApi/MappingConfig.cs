using AutoMapper;
using FlightInvoice.SftpFileApi.Models;
using FlightInvoice.SftpFileApi.Models.Dto;

namespace FlightInvoice.SftpFileApi;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<SftpFileDto, SftpFile>();
            config.CreateMap<SftpFile, SftpFileDto>();
        });

        return mappingConfig;
    }
}

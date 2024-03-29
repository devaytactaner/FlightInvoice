using FlightInvoice.SftpReader.Models;
using FlightInvoice.SftpReader.Models.Dto;
using FlightInvoice.SftpReader.Service.IService;
using FlightInvoice.SftpReader.Utility;

namespace FlightInvoice.SftpReader.Service;

public class SftpFileService : ISftpFileService
{
    private readonly IBaseService _baseService;

    public SftpFileService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto?> CreateSftpFileAsync(SftpFileDto dto)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = SD.SftpFileApiBase + "/api/SftpFileApi"
        });
    }

    public async Task<ResponseDto?> DeleteSftpFileAsync(string path)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.SftpFileApiBase + "/api/SftpFileApi"
        });
    }

    public async Task<ResponseDto?> GetSftpFileAsync(string path)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.SftpFileApiBase + "/api/SftpFileApi/" + path
        });
    }

    public async Task<ResponseDto?> GetSftpFilesAsync()
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.SftpFileApiBase + "/api/SftpFileApi"
        });
    }

    public async Task<ResponseDto?> UpdateSftpFileAsync(SftpFileDto dto)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.PUT,
            Data = dto,
            Url = SD.SftpFileApiBase + "/api/SftpFileApi"
        });
    }
}

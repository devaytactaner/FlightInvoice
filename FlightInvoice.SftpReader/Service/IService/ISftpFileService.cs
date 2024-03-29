using FlightInvoice.SftpReader.Models;
using FlightInvoice.SftpReader.Models.Dto;

namespace FlightInvoice.SftpReader.Service.IService
{
    public  interface ISftpFileService
    {
        Task<ResponseDto?> GetSftpFilesAsync();
        Task<ResponseDto?> GetSftpFileAsync(string path);
        Task<ResponseDto?> CreateSftpFileAsync(SftpFileDto dto);
        Task<ResponseDto?> UpdateSftpFileAsync(SftpFileDto dto);
        Task<ResponseDto?> DeleteSftpFileAsync(string path);

    }
}

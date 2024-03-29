using FlightInvoice.SftpReader.Models;

namespace FlightInvoice.SftpReader.Service.IService;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}

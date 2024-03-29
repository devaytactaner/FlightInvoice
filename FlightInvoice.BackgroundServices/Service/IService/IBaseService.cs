using FlightInvoice.BackgroundServices.Models;

namespace FlightInvoice.BackgroundServices.Service.IService;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}

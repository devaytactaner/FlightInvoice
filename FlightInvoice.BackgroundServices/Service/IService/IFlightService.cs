using FlightInvoice.BackgroundServices.Models;
using FlightInvoice.BackgroundServices.Models.Dto;

namespace FlightInvoice.BackgroundServices.Service.IService;

public interface IFlightService
{
    Task<ResponseDto?> GetFlightAsync();
    Task<ResponseDto?> GetFlightAsync(string carrierCode, int flightNo, DateTime flightDate);
    Task<ResponseDto?> UpdateFlightAsync(string carrierCode, int flightNo, string flightDate, double flightPrice, int invoiceNumber);


}

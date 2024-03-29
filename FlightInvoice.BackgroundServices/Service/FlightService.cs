using FlightInvoice.BackgroundServices.Models;
using FlightInvoice.BackgroundServices.Models.Dto;
using FlightInvoice.BackgroundServices.Service.IService;
using FlightInvoice.BackgroundServices.Utility;

namespace FlightInvoice.BackgroundServices.Service;

public class FlightService : IFlightService
{
    private readonly IBaseService _baseService;

    public FlightService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> GetFlightAsync()
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.FlightApiBase + "/api/flight"
        });
    }

    public async Task<ResponseDto?> GetFlightAsync(string carrierCode, int flightNo, DateTime flightDate)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.FlightApiBase + "/api/flight/" + carrierCode + "/" + flightNo + "/" + flightDate
        });
    }

    public async Task<ResponseDto?> UpdateFlightAsync(string carrierCode, int flightNo, string flightDate, double flightPrice, int invoiceNumber)
    {
        return await _baseService.SendAsync(new()
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.FlightApiBase + "/api/flight/" + carrierCode + "/" + flightNo + "/" + flightDate + "/" + flightPrice + "/" + invoiceNumber
        });
    }
}

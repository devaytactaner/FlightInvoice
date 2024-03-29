using AutoMapper;
using FlightInvoice.FlightApi.Data;
using FlightInvoice.FlightApi.Models;
using FlightInvoice.FlightApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FlightInvoice.FlightApi.Controllers;

[Route("api/flight")]
[ApiController]
public class FlightApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;

    public FlightApiController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _response = new ResponseDto();
        _mapper = mapper;
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Flight> flights = _db.Flight.ToList();
            _response.Result = _mapper.Map<IEnumerable<FlightDto>>(flights);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{carrierCode}/{flightNo:int}/{flightDate}")]
    public ResponseDto Get(string carrierCode, int flightNo, string flightDate)
    {
        try
        {
            IEnumerable<Flight> flights = _db.Flight.Where(r => r.CarrierCode == carrierCode && r.FlightNo == flightNo && r.FlightDate == Convert.ToDateTime(flightDate)).ToList();
            _response.Result = _mapper.Map<IEnumerable<FlightDto>>(flights);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPut]
    [Route("{carrierCode}/{flightNo:int}/{flightDate}/{flightPrice:float}/{invoiceNumber:int}")]
    public ResponseDto Put(string carrierCode, int flightNo, string flightDate, double flightPrice, int invoiceNumber)
    {
        try
        {            
            Flight dbflight = _db.Flight.Where(r =>
            r.CarrierCode == carrierCode &&
            r.FlightNo == flightNo &&
            r.FlightDate == Convert.ToDateTime(flightDate) &&
            r.InvoiceNumber == null &&
            r.Price == flightPrice
            ).FirstOrDefault()!;

            if (dbflight == null)
                throw new Exception("No record");

            dbflight.InvoiceNumber = invoiceNumber;

            _db.Flight.Update(dbflight);
            _db.SaveChanges();

            _response.Result = null;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}

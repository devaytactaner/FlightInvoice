using AutoMapper;
using FlightInvoice.InvoiceApi.Data;
using FlightInvoice.InvoiceApi.Models;
using FlightInvoice.InvoiceApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightInvoice.InvoiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public InvoiceApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{invoiceId:int}/{invoiceDate:datetime}")]
        public ResponseDto Get(int invoiceId, DateTime invoiceDate)
        {
            try
            {
                IEnumerable<Invoice> invoices = _db.Invoice.Where(r => r.Id == invoiceId && r.Date == invoiceDate).ToList();
                _response.Result = _mapper.Map<Invoice>(invoices.First());
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                Invoice invoice = _mapper.Map<Invoice>(invoiceDto);
                _db.Invoice.Add(invoice);
                _db.SaveChanges();
                _response.Result = _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int invoiceId, DateTime invoiceDate)
        {
            try
            {
                Invoice invoice = _db.Invoice.First(r => r.Id == invoiceId && r.Date == invoiceDate);
                _db.Invoice.Remove(invoice);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}

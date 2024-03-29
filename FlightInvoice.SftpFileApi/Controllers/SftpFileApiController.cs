using AutoMapper;
using FlightInvoice.SftpFileApi.Data;
using FlightInvoice.SftpFileApi.Models;
using FlightInvoice.SftpFileApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightInvoice.SftpFileApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SftpFileApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;

    public SftpFileApiController(AppDbContext db, IMapper mapper)
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
            IEnumerable<SftpFile> sftpFiles = _db.SftpFile.ToList();
            _response.Result = _mapper.Map<IEnumerable<SftpFile>>(sftpFiles);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{path}")]
    public ResponseDto Get(string path)
    {
        try
        {
            IEnumerable<SftpFile> sftpFile = _db.SftpFile.Where(r => r.Path == path).ToList();
            _response.Result = _mapper.Map<SftpFile>(sftpFile.First());
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPost]
    public ResponseDto Post([FromBody] SftpFileDto invoiceDto)
    {
        try
        {
            SftpFile sftpFile = _mapper.Map<SftpFile>(invoiceDto);
            _db.SftpFile.Remove(sftpFile);
            _db.SaveChanges();
            _db.SftpFile.Add(sftpFile);
            _db.SaveChanges();
            _response.Result = _mapper.Map<SftpFileDto>(sftpFile);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpDelete]
    public ResponseDto Delete(string path)
    {
        try
        {
            SftpFile invoice = _db.SftpFile.First(r => r.Path == path);
            _db.SftpFile.Remove(invoice);
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

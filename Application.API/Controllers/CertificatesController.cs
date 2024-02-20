using Application.Contracts.Logger;
using Application.Contracts.UnitOfWork;
using Application.Domain.Models;
using Application.SharedDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CertificatesController(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILoggerManager loggerManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        [HttpGet("get-all-certificate")]
        public IActionResult GetAllCertificates() 
        {
            try
            {
                var certificate = _unitOfWorkRepository.Certificate.GetAll();
                _loggerManager.LogInfo("Return all certificate from database");
                var result = _mapper.Map<IEnumerable<CertificateDTO>>(certificate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetAllCertificate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-certificate-by-id")]
        public IActionResult GetCertificateById(int id) 
        {
            try
            {
                var certificate = _unitOfWorkRepository.Certificate.GetByCondition(x => x.CertificateId == id).FirstOrDefault();
                if (certificate == null)
                {
                    _loggerManager.LogError($"Certificate with id: {id} hasn't been found in database");
                    return NotFound();
                }
                else 
                {
                    var result = _mapper.Map<CertificateDTO>(certificate);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetCertificateById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("add-certificate")]
        public IActionResult AddCertificate(CertificateDTO certificate) 
        {
            try
            {
                if (certificate is null)
                {
                    _loggerManager.LogError("Certificate object sent from client is null");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid certificate object sent from client");
                    return BadRequest("Invalid model object");
                }
                var certificateEntity = _mapper.Map<Certificate>(certificate);
                _unitOfWorkRepository.Certificate.SaveCertificate(certificateEntity);
                _unitOfWorkRepository.Save();

                var createdCertificate = _mapper.Map<Certificate>(certificate);
                return CreatedAtAction("GetCertificateById", new { id = createdCertificate.CertificateId }, createdCertificate);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddCertificate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update-certificate")]
        public IActionResult UpdateCertificate(int id, CertificateDTO certificate) 
        {
            try
            {
                if (certificate is null)
                {
                    _loggerManager.LogError("Certificate object sent from client is null");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid certificate object sent from client");
                    return BadRequest("Invalid model object");
                }
                var certificateId = _unitOfWorkRepository.Certificate.GetByCondition(x=> x.CertificateId == id).FirstOrDefault();
                if (certificateId == null) 
                {
                    _loggerManager.LogError($"Certificate with the id: {id} hasn't been found in db");
                    return NotFound();
                }
                var result = _mapper.Map(certificate, certificateId);
                _unitOfWorkRepository.Certificate.Update(result);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside the UpdateCertificate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("delete-certificate")]
        public IActionResult DeleteCertificate(int id) 
        {
            try
            {
                var certificate = _unitOfWorkRepository.Certificate.GetByCondition(x => x.CertificateId == id).FirstOrDefault();
                if (certificate == null) 
                {
                    _loggerManager.LogError($"Certificate with this id: {id} hasn't been found in db");
                    return NotFound();
                }
                _unitOfWorkRepository.Certificate.DeleteCertificate(certificate);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside DeleteCertificate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

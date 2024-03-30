using Application.Contracts.Logger;
using Application.Contracts.UnitOfWork;
using Application.Domain.Models;
using Application.Infrastructure.Logger;
using Application.SharedDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManger;
        public RegistrationController(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILoggerManager loggerManger)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _loggerManger = loggerManger;
        }
        [HttpGet("get-all-user")]
        public IActionResult GetAllUsers() 
        {
            try
            {
                var user = _unitOfWorkRepository.Registration.GetAll();
                _loggerManger.LogInfo("Return all user from database");
                var result = _mapper.Map<IEnumerable<UsersDTO>>(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _loggerManger.LogError($"Something went worng inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-all-user-by-id")]
        public IActionResult GetUserById(int id) 
        {
            try
            {
                var user = _unitOfWorkRepository.Registration.GetByCondition(x => x.UsersId == id).FirstOrDefault();
                if (user == null)
                {
                    _loggerManger.LogError($"User with id: {id} hasn't been found in database");
                    return NotFound();
                }
                else
                {
                    var result = _mapper.Map<UsersDTO>(user);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _loggerManger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("add-user")]
        public IActionResult AddUser(UsersDTO user)
        {
            try
            {
                var userExists = _unitOfWorkRepository.Registration.GetByCondition(x => x.UserName == user.UserName).FirstOrDefault();
                if (userExists != null)
                {
                    return BadRequest("User is already exists!!");
                }
                if (user is null) 
                {
                    _loggerManger.LogError("User object sent from client is null");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid) 
                {
                    _loggerManger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }
                var users = _mapper.Map<Users>(user);
                _unitOfWorkRepository.Registration.SaveUser(users);
                _unitOfWorkRepository.Save();

                return CreatedAtAction("GetUserById", new { id=users.UsersId}, users);
            }
            catch (Exception ex)
            {
                _loggerManger.LogError($"Something went wrong inside AddUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update-user")]
        public IActionResult UpdateUser(int id, UsersDTO user) 
        {
            try
            {
                if (user is null) 
                {
                    _loggerManger.LogError("User object is null");
                    return BadRequest("Object is null");
                }
                if (!ModelState.IsValid) 
                {
                    _loggerManger.LogError("Invalid user object sent from cliend");
                    return BadRequest("Invalid user object");
                }
                var usersId = _unitOfWorkRepository.Registration.GetByCondition(x => x.UsersId == id).FirstOrDefault();
                if (usersId == null)
                {
                    _loggerManger.LogError($"{usersId} is not registered.");
                    return BadRequest("User name isn't in db");
                }
                var result = _mapper.Map(user, usersId);
                _unitOfWorkRepository.Registration.UpdateUser(result);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("delete-user")]
        public IActionResult DeleteUser(int id) 
        {
            try
            {
                var user = _unitOfWorkRepository.Registration.GetByCondition(x => x.UsersId == id).FirstOrDefault();
                if (user == null)
                {
                    _loggerManger.LogError($"Certificate with this id: {id} hasn't been found in db");
                    return NotFound();
                }
                _unitOfWorkRepository.Registration.DeleteUser(user);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManger.LogError($"Something went wrong inside DeleteCertificate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

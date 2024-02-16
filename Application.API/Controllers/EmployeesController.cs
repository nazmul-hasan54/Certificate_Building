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
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public EmployeesController(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILoggerManager loggerManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        [HttpGet("get-all-employee")]
        public IActionResult GetAllEmployees() 
        {
            try
            {
                var employee = _unitOfWorkRepository.Employee.GetAll();
                _loggerManager.LogInfo("Return all employee from database");
                if (employee == null)
                {
                    _loggerManager.LogError($"No data found in database");
                    return BadRequest("Employee object is null");
                }
                else 
                {
                    var result = _mapper.Map<IEnumerable<EmployeeDTO>>(employee).ToList();
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetAllEmployees action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-employee-by-id")]
        public IActionResult GetEmployeeById(int id) 
        {
            try
            {
                var employee = _unitOfWorkRepository.Employee.GetByCondition(x=> x.EmployeeId==id).FirstOrDefault();
                if (employee == null)
                {
                    _loggerManager.LogError($"Employee with id: {id} hasn't been found in db");
                    return NotFound();
                }
                else 
                {
                    var result = _mapper.Map<EmployeeDTO>(employee);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500,"Internal server errror");
            }
        }
        [HttpPost("add-employee")]
        public IActionResult SaveEmployee(EmployeeDTO employee) 
        {
            try
            {
                if (employee is null)
                {
                    _loggerManager.LogError("Department object sent from client is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid department object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var employeeEntity = _mapper.Map<Employee>(employee);
                _unitOfWorkRepository.Employee.SaveEmployee(employeeEntity);
                _unitOfWorkRepository.Save();

                var createdEmployee = _mapper.Map<EmployeeDTO>(employee);
                return CreatedAtAction("GetEmployeeById", new { id=createdEmployee.EmployeeId}, createdEmployee);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside SaveEmployee action: {ex.Message}");
                return StatusCode(500,"Internal server error");
            }
        }
        [HttpPut("update-employee")]
        public IActionResult UpdateEmployee(int id, EmployeeDTO employee) 
        {
            try
            {
                if (employee is null)
                {
                    _loggerManager.LogError("Department object sent from client is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid department object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var employeeId = _unitOfWorkRepository.Employee.GetByCondition(x=> x.EmployeeId==id).FirstOrDefault();
                if (employeeId == null) 
                {
                    _loggerManager.LogError($"Department with id: {id} hasn't been found in db");
                    return NotFound();
                }
                var result = _mapper.Map(employee, employeeId);
                _unitOfWorkRepository.Employee.UpdateEmployee(result);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500,"Internal server error");
            }
        }
        [HttpDelete("delete-employee")]
        public IActionResult DeleteEmployee(int id) 
        {
            try
            {
                var employee = _unitOfWorkRepository.Employee.GetByCondition(x=> x.EmployeeId==id).FirstOrDefault();
                if (employee == null)
                {
                    _loggerManager.LogError($"Department with id: {id} hasn't been with the db");
                    return NotFound();
                }
                _unitOfWorkRepository.Employee.DeleteEmployee(employee);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside the DeleteDepartment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

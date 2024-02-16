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
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public DepartmentsController(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILoggerManager loggerManager)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        [HttpGet("get-all-departments")]
        public IActionResult GetAllDepartments() 
        {
            try
            {
                var departments = _unitOfWorkRepository.Department.GetAll();
                _loggerManager.LogInfo($"Return all department from database");
                var result = _mapper.Map<IEnumerable<DepartmentDTO>>(departments).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetAllDepartments action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("get-department-by-id")]
        public IActionResult GetDepartmentById(int id) 
        {
            try
            {
                var department = _unitOfWorkRepository.Department.GetByCondition(x=> x.DepartmentId==id).FirstOrDefault();
                if (department == null)
                {
                    _loggerManager.LogError($"Department with id: {id} hasn't been found in db");
                    return NotFound();
                }
                else 
                {
                    _loggerManager.LogInfo($"Return department with id: {id}");
                    var result = _mapper.Map<DepartmentDTO>(department);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside GetDepartmentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("add-department")]
        public IActionResult SaveDepartment(DepartmentDTO department) 
        {
            try
            {
                if (department is null) 
                {
                    _loggerManager.LogError("Department object sent from client is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid) 
                {
                    _loggerManager.LogError("Invalid department object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var departmentEntity = _mapper.Map<Department>(department);
                _unitOfWorkRepository.Department.SaveDepartment(departmentEntity);
                _unitOfWorkRepository.Save();

                var createdDepartment = _mapper.Map<DepartmentDTO>(department);
                return CreatedAtAction("GetDepartmentById", new { id = createdDepartment.DepartmentId }, createdDepartment);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside the SaveDepartment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update-department")]
        public IActionResult UpdateDepartment(int id, DepartmentDTO department) 
        {
            try
            {
                if (department is null) 
                {
                    _loggerManager.LogError("Department object sent from client is null.");
                    return BadRequest("Department object is null");
                }
                if (!ModelState.IsValid) 
                {
                    _loggerManager.LogError("Invalid department object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var departmentId = _unitOfWorkRepository.Department.GetByCondition(x=> x.DepartmentId==id).FirstOrDefault();
                if (departmentId == null) 
                {
                    _loggerManager.LogError($"Department with id: {id} hasn't been found in db");
                    return NotFound();
                }
                var result = _mapper.Map(department,departmentId);
                _unitOfWorkRepository.Department.UpdateDepartment(result);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside UpdateDepartment action: {ex.Message}");
                return StatusCode(500,"Internal server error");
            }
        }
        [HttpDelete("delete-department")]
        public IActionResult DeleteDepartment(int id) 
        {
            try
            {
                var department = _unitOfWorkRepository.Department.GetByCondition(x=> x.DepartmentId==id).FirstOrDefault();
                if (department == null) 
                {
                    _loggerManager.LogError($"Department with id: {id} hasn't been with the db");
                    return NotFound();
                }
                _unitOfWorkRepository.Department.DeleteDepartment(department);
                _unitOfWorkRepository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside the DeleteDepartment action: {ex.Message}");
                return StatusCode(500,"Internal server error");
            }
        }

    }
}

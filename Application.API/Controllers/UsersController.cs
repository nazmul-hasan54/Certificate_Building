using Application.Contracts.UnitOfWork;
using Application.Domain.Models;
using Application.SharedDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        public UsersController(IUnitOfWorkRepository unitOfWorkRepository, 
            IMapper mapper, UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> _optionMonitor)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _userManager = userManager;
            _jwtConfig = _optionMonitor.CurrentValue;
        }
        [HttpPost("user-registration")]
        public IActionResult UserRegistration([FromBody]UserRegistrationRequestDTO request) 
        {
            //var emailExist = _userManager.FindByEmailAsync(request.Email);

            //if (emailExist != null)
            //    return BadRequest("Email already exists");

            if (ModelState.IsValid) 
            {

                var newUser = new IdentityUser()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                };

                 _userManager.CreateAsync(newUser, request.Password);
                _unitOfWorkRepository.Save();
                return Ok();

            }
            return BadRequest("Invalid request payload");
        }
    }
}

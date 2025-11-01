using AutoMapper;
using HajorPay.ThriftService.Application.DTOs;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HajorPay.ThriftService.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AccountsController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUserAsync(UserRegistrationDto userForRegistrationDto)
        {
            if (userForRegistrationDto == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<ApplicationUser>(userForRegistrationDto);
            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            return Ok("Ping!");
        }
    }
}

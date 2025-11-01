using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HajorPay.ThriftService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                NIN = request.NIN,
                BVN = request.BVN,
                Password = request.Password,
                OptInForSMS = request.OptInForSMS
            };

            return Ok(await _sender.Send(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginUserCommand()
            {
                Username = request.Username,
                Password = request.Password
            };

            return Ok(await _sender.Send(command));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand()
            {
                RefreshToken = request.Refresh
            };

            return Ok(await _sender.Send(command));
        }
    }
}

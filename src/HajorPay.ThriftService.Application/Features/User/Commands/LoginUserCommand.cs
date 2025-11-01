using HajorPay.ThriftService.Application.Abstractions.Identity;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Wrappers;
using HajorPay.ThriftService.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application.Features.User.Commands
{
    public class LoginUserCommand:IRequest<Response<LoginDto>>
    {
        public string Username { get; init; } = default!;
        public string Password { get; init; } = default!;

    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<LoginDto>>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginDto = await _userService.LoginAsync(request);
            //_logger.LogInformation("New user created with Id ({UserId}) for Email Address ({EmailAddress})", userDto.Id, userDto.EmailAddress);

            return Response<LoginDto>.Success(loginDto, "User logged in successfully!");

        }
    }
}

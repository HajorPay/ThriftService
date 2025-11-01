using HajorPay.ThriftService.Application.Abstractions.Identity;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application.Features.User.Commands
{
    public class RefreshTokenCommand : IRequest<Response<RefreshTokenDto>>
    {
        public string RefreshToken { get; set; } = default!;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<RefreshTokenDto>>
    {
        private readonly IUserService _userService;

        public RefreshTokenCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<RefreshTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenDto = await _userService.RefreshTokenAsync(request);
            //_logger.LogInformation("New user created with Id ({UserId}) for Email Address ({EmailAddress})", userDto.Id, userDto.EmailAddress);

            return Response<RefreshTokenDto>.Success(refreshTokenDto, "Refresh token retrieved successfully!");

        }
    }
}

using HajorPay.ThriftService.Application.Abstractions.Identity;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Wrappers;
using HajorPay.ThriftService.Domain.Constants;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HajorPay.ThriftService.Application.Features.User.Commands
{
    public record CreateUserCommand : IRequest<Response<UserDto>>
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string BVN { get; init; } = default!;
        public string NIN { get; init; } = default!;
        public bool RegisteredByAdmin { get; init; } = false;
        public bool OptInForEmail { get; init; } = true;
        public bool OptInForSMS { get; init; } = false;
    }


    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IUserService userService, ILogger<CreateUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = await _userService.CreateUserAsync(request);
            _logger.LogInformation("New user created with Id ({UserId}) for Email Address ({EmailAddress})", userDto.Id, userDto.EmailAddress);

            return Response<UserDto>.Success(userDto, ResponseMessages.UserCreated);
        }
    }
}

using HajorPay.ThriftService.Application.DTOs.Identity;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Features.User.Commands;

namespace HajorPay.ThriftService.Application.Abstractions.Identity
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserCommand command);
        Task<LoginDto> LoginAsync(LoginUserCommand command);
        Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenCommand command);

        //Task<ApplicationUserDto?> FindByEmailAsync(string email);
    }
}

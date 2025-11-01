using HajorPay.ThriftService.Application.Interfaces.Identity;

namespace HajorPay.ThriftService.Application.Abstractions.Identity
{
    public interface ITokenService
    {
        string CreateToken(IUserIdentity user);
        string CreateRefreshToken();
    }
}

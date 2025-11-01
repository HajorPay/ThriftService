using HajorPay.ThriftService.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace HajorPay.ThriftService.Infrastructure.Identity.Validators
{
    public class AppNameResemblesPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            if (password.Contains(DomainConstants.AppShortName, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "AppNameResemblesPassword",
                    Description = "You cannot use a password that resembles the name of this app."

                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

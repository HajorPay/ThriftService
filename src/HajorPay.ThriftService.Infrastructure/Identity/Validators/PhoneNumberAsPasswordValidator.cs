using Microsoft.AspNetCore.Identity;

namespace HajorPay.ThriftService.Infrastructure.Identity.Validators
{
    public class PhoneNumberAsPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (string.Equals(user.PhoneNumber, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError()
                    {
                        Code = "PhoneNumberAsPassword",
                        Description = "You cannot use your phone number as your password"
                    }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

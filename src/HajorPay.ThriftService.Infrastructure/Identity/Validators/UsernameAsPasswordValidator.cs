using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Infrastructure.Identity.Validators
{
    public class UsernameAsPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase) || string.Equals(user.Email, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = "You cannot use your username or email as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

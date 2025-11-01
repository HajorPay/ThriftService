using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Infrastructure.Identity.Validators
{
    public class Top1000PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        private static readonly HashSet<string> Passwords = PasswordLists.PasswordLists.Top1000Passwords;

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (Passwords.Contains(password))
            {
                var result = IdentityResult.Failed(new IdentityError
                {
                    Code = "CommonPassword",
                    Description = "The password you chose is too common."
                });
                return Task.FromResult(result);
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}

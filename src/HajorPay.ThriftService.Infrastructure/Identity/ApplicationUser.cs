
using HajorPay.ThriftService.Application.Interfaces.Identity;
using HajorPay.ThriftService.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace HajorPay.ThriftService.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser<Guid>, IUserIdentity
    {
        //public ApplicationUser()
        //{
        //    FirstName = string.Empty;
        //    LastName = string.Empty;
        //    BVN = string.Empty;
        //}

        //public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //public required string PhoneNumber { get; set; }
        //public string? EmailAddress { get; set; } //Todo: It should be compulsory for Admins. Identity has Email
        public string BVN { get; set; } = string.Empty;
        public string? NIN { get; set; }
        public bool OptInForSMS { get; set; }
        public bool OptInForEmail { get; set; }
        public bool RegisteredByAdmin { get; set; }
        public bool IsActive { get; set; }
        //public List<RefreshToken> RefreshTokens { get; set; } = new();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
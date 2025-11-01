using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Domain.Entities.Auth
{
    public class RefreshToken
    {

        public Guid Id { get; set; }
        public string Token { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        //public bool IsExpired => DateTime.UtcNow >= ExpiresOnUtc;
        //public bool IsActive => Revoked == null && !IsExpired;

    }
}

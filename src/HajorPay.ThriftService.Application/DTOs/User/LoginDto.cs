using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application.DTOs.User
{
    public class LoginDto
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

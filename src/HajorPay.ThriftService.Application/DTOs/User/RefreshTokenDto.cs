using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application.DTOs.User
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}

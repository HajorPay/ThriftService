using HajorPay.ThriftService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application.Services
{
    public class VirtualAccountService : IVirtualAccountService
    {
        public string GetANewVirtualAccount()
        {
            var virtualAccount = GenerateRandomTenDigitNumber();
            return virtualAccount;
        }
        private string GenerateRandomTenDigitNumber()
        {
            //Call Virtual Account Provider
            Random random = new Random();
            return random.Next(1000000000, int.MaxValue).ToString();
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace HajorPay.ThriftService.API.Common.Extensions
{
    public static class ExceptionExtensions
    {
        private const string ErrorCodeKey = "ErrorCode";

        public static Exception AddErrorCode(this Exception exception)
        {
            using var sha1 = SHA1.Create();
            var message = exception.Message ?? "Unknown error";
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            var errorCode = string.Concat(hash[..5].Select(b => b.ToString("x")));
            exception.Data[ErrorCodeKey] = errorCode;
            return exception;
        }

        public static string GetErrorCode(this Exception exception)
        {
            return exception.Data.Contains(ErrorCodeKey) ? (string)exception.Data[ErrorCodeKey]! : "NoErrorCode";
        }

        public static IDictionary<string, string[]> GetValidationErrors(this Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                return validationException.GetValidationErrors();
            }

            return new Dictionary<string, string[]>(); // Empty if not validation-related
        }
    }
}

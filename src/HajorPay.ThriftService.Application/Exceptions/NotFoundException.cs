namespace HajorPay.ThriftService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Record not found.") { }

        public NotFoundException(string message) : base(message) { }
    }
}

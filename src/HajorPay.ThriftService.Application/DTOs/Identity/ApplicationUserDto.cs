namespace HajorPay.ThriftService.Application.DTOs.Identity
{
    public class ApplicationUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string BVN { get; set; }
        public required string NIN { get; set; }
        public bool OptInForSMS { get; set; }
        public bool OptInForEmail { get; set; }
        public bool RegisteredByAdmin { get; set; }
    }
}

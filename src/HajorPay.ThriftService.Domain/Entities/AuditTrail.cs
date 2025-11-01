using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Domain.Entities
{
    public class AuditTrail
    {
        public long Id { get; set; }
        public AuditType Type { get; set; }
        public DateTimeOffset DateChanged { get; set; }
        public required string Details { get; set; }
    }
}
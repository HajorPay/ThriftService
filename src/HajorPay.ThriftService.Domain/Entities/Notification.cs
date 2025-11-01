using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Domain.Entities
{
    public class Notification
    {
        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public required Group Group { get; set; }
        public required string Message { get; set; }
        public bool Sent { get; set; }
        public NotificationType Type { get; set; }
    }
}
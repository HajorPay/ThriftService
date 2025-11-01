namespace HajorPay.ThriftService.Domain.Entities
{
    public class Contributor
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public required Group Group { get; set; }
        public DateTimeOffset DateJoined { get; set; }
        public required string ContributionAccount { get; set; }
        public required string DisbursementAccount { get; set; }
        public bool MadeContribution { get; set; }
        public bool ReceivedPayment { get; set; }
        public int PayoutOrder { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool DefaultingInCurrentCycle { get; set; }
        public int DefaultedCount { get; set; }
        public bool CanPickNumber { get; set; }

    }
}
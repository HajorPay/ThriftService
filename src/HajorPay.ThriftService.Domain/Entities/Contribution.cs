using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Domain.Entities
{
    public class Contribution
    {
        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public required Group Group { get; set; }
        public decimal Amount { get; set; }
        public int Cycle { get; set; }
        public ContributionStatus Status { get; set; }
        public DateTimeOffset NextContributionDate { get; set; }
        public DateTimeOffset NextPayoutDate { get; set; }
    }
}
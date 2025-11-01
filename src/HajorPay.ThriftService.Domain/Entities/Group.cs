using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; } //ApplicationUserId
        //public required User Admin { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string ContributionAccount { get; set; }
        public required decimal ContributionAmount { get; set; }
        public GroupStatus GroupStatus { get; set; }
        public ContributionFrequency ContributionFrequency { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal TotalContributions { get; set; }
        public decimal LateFee { get; set; }
        public int NumberOfContributors { get; set; }
        public ICollection<Contributor> Contributors { get; set; }
    }
}
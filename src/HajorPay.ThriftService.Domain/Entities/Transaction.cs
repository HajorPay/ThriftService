using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Domain.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public required Group Group { get; set; }
        public long ContributorId { get; set; }
        public required string SourceAccount { get; set; }
        public required string DestinationAccount { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public required string Remarks { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}

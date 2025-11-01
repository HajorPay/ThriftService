using HajorPay.ThriftService.Domain.Entities;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HajorPay.ThriftService.Infrastructure.Persistence.Configurations
{
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(x => x.AdminId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ContributionAmount)
                .HasPrecision(18, 4);
            builder.Property(x => x.LateFee)
                .HasPrecision(18, 4);
            builder.Property(x => x.TotalContributions)
                .HasPrecision(18, 4);
        }
    }
}

using HajorPay.ThriftService.Domain.Entities;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HajorPay.ThriftService.Infrastructure.Persistence.Configurations
{
    public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
    {
        public void Configure(EntityTypeBuilder<Contributor> builder)
        {
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Group)
                   .WithMany(x => x.Contributors)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasAlternateKey(x => x.ContributionAccount);
        }
    }
}

using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HajorPay.ThriftService.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.Property(u => u.BVN)
                .IsRequired();
        }
    }
}

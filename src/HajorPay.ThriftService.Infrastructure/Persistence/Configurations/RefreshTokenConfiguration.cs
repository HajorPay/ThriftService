using HajorPay.ThriftService.Domain.Entities.Auth;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).HasMaxLength(200); //TODO: In appDbContex, this is handle with reflection

            builder
                .HasOne<ApplicationUser>() // Identity user
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
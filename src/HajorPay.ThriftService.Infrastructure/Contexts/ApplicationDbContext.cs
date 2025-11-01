using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Domain.Constants;
using HajorPay.ThriftService.Domain.Entities;
using HajorPay.ThriftService.Domain.Entities.Auth;
using HajorPay.ThriftService.Infrastructure.Data;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HajorPay.ThriftService.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>, IHajorPayDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {
            
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #region Repositories
        public IHajorPayRepository<Group> Group => new HajorPayRepository<Group>(this);


        public async Task<bool> ExecuteAnyAsync<TEntity>(IQueryable<TEntity> query) => await query.AnyAsync();

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await SaveChangesAsync(cancellationToken);


        #endregion




        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);

            // Global string length configuration
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string) && property.GetMaxLength() == null)
                    {
                        property.SetColumnType("varchar(255)"); // Default varchar(255) for all string properties
                    }
                }
            }

            // Seed roles
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = DomainConstants.Role.Admin,
                    NormalizedName = DomainConstants.Role.Admin.ToUpperInvariant()
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = DomainConstants.Role.User,
                    NormalizedName = DomainConstants.Role.User.ToUpperInvariant()
                }
            );
        }

    }
}

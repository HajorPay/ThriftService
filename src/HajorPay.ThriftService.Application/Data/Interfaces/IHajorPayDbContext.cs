using HajorPay.ThriftService.Domain.Entities;

namespace HajorPay.ThriftService.Application.Data.Interfaces
{
    public interface IHajorPayDbContext
    {
        IHajorPayRepository<Group> Group { get; }


        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        Task<bool> ExecuteAnyAsync<TEntity>(IQueryable<TEntity> query);


    }
}

namespace HajorPay.ThriftService.Application.Data.Interfaces
{
    public interface IHajorPayRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable();
        Task AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync<TId>(TId id);

    }
}

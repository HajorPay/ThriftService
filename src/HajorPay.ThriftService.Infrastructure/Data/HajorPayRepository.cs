using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HajorPay.ThriftService.Infrastructure.Data
{
    public class HajorPayRepository<TEntity>(ApplicationDbContext dbContext) : IHajorPayRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public IQueryable<TEntity> AsQueryable() => _dbSet;
        
        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public async Task<TEntity?> GetByIdAsync<TId>(TId id) => await _dbSet.FindAsync(id);
        //public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    }
}

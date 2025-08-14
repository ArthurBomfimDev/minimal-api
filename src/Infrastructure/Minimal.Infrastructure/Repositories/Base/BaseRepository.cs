using Microsoft.EntityFrameworkCore;
using Minimal.Application.Interfaces.Repositories.Base;
using Minimal.Domain.Entities.Base;
using Minimal.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Minimal.Infrastructure.Repositories.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity<TEntity>
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(new object[] { id });
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task CreateMultiple(List<TEntity> listEntity)
    {
        await _dbSet.AddRangeAsync(listEntity);
    }

    public void Update(List<TEntity> listEntity)
    {
        _dbSet.UpdateRange(listEntity);
    }

    public void Remove(List<TEntity> listEntity)
    {
        _dbSet.RemoveRange(listEntity);
    }
}
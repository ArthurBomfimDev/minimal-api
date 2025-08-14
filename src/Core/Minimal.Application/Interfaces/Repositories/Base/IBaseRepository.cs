using Minimal.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Minimal.Application.Interfaces.Repositories.Base;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity<TEntity>
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task Create(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task CreateMultiple(List<TEntity> listEntity);
    void Update(List<TEntity> listEntity);
    void Remove(List<TEntity> listEntity);

}
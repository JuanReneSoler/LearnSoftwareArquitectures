using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity, TEntityID>
    where TEntity : BaseEntity<TEntityID>
{
    Task<bool> Exist(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    Task Add(TEntity Entity, CancellationToken cancellationToken);
    Task Update(TEntity Entity, CancellationToken cancellationToken);
    Task Delete(TEntityID Id, CancellationToken cancellationToken);
    Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity> Find(TEntityID Id, CancellationToken cancellationToken);
}

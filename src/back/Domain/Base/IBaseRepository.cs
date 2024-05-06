using System.Linq.Expressions;

namespace Domain.Base;

public interface IBaseRepository<TEntity, TEntityID>
    where TEntity : BaseEntity<TEntityID>
{
    Task Add(TEntity Entity, CancellationToken cancellationToken);
    Task Update(TEntity Entity, CancellationToken cancellationToken);
    Task Delete(TEntityID Id, CancellationToken cancellationToken);
    Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, int? skip, int? take, CancellationToken cancellationToken);
    //
    Task<bool> Commit(CancellationToken cancellationToken);
    Task Rollback(CancellationToken cancellationToken);
}

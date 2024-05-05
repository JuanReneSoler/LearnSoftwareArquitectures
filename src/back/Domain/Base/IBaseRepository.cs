using System.Linq.Expressions;

namespace Domain.Base;

public interface IBaseRepository<TEntity, TEntityID>
    where TEntity : BaseEntity<TEntityID>
{
    Task Add(TEntity Entity);
    Task Update(TEntity Entity);
    Task Delete(TEntityID Id);
    Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
    //
    Task<bool> Commit();
    Task Rollback();
}

using Domain.Base;

using System.Linq.Expressions;

namespace Application;

public interface IGenericService<TEntity, TEntityID>
    where TEntity : BaseEntity
{
    TEntity Add(TEntity Entity);
    void Delete(TEntityID Id);
    void Update(TEntity Entity, TEntityID Id);
    IList<TEntity> GetAll(int? skip, int? take);
    IList<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
}

using System.Linq.Expressions;

namespace Application;

public interface IGenericService<TEntity, TEntityID>
    where TEntity : class
{
    TEntity? Add(TEntity Entity);
    TEntityID Delete(TEntityID Id);
    TEntity? Update(TEntity Entity, TEntityID Id);
    IList<TEntity> GetAll(int? skip, int? take);
    IList<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
}

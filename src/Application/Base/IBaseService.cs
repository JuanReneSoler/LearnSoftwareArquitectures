using System.Linq.Expressions;

namespace Application.Base;

public interface IBaseService<TEntity, TEntityID>
    where TEntity : DtoBase<TEntityID>
{
    TEntity? Create(TEntity Entity);
    TEntityID Delete(TEntityID Id);
    TEntity? Update(TEntity Entity, TEntityID Id);
    IList<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
}

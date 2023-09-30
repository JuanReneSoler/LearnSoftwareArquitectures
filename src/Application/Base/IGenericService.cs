using System.Linq.Expressions;

namespace Domain;

public interface IGenericService<TEntity>
    where TEntity : BaseEntity
{
    TEntity Add(TEntity Entity);
    void Delete(int Id);
    void Update(TEntity Entity, int Id);
    IList<TEntity> GetAll(int? skip, int? take);
    IList<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
}

using System.Linq.Expressions;

namespace Domain;

public interface IGenericService<TEntity>
    where TEntity : IEntity
{
    TEntity Add(TEntity Entity);
    void Delete(int Id);
    void Update(TEntity Entity, int Id);
    IList<TEntity> GetAll();
    IList<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
}

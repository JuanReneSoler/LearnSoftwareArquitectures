using System.Linq.Expressions;

namespace Domain;

public interface IGenericRepository<TEntity> 
    where TEntity : IEntity
{
    TEntity Add(TEntity Entity);
    void AddRange(TEntity[] Entities);
    //
    void Update(TEntity Entity);
    //
    void Delete(int Id);
    void DeleteRange(int[] Ids);
    //
    TEntity Get(int Id);
    TEntity Get(Expression<Func<TEntity, bool>> predicate);
    //
    IList<TEntity> GetAll();
    IList<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    //
    void Save();
}

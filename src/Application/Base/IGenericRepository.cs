using Domain.Base;

using System.Linq.Expressions;

namespace Domain;

public interface IGenericRepository<TEntity> 
    where TEntity : BaseEntity
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
    IList<TEntity> GetAll(int? skip, int? take);
    IList<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
    //
    void Commit();
    void Rollback();
}

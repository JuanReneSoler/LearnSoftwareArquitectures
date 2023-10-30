using Domain.Base;

using System.Linq.Expressions;

namespace Domain;

public interface IGenericRepository<TEntity, TEntityID>
    where TEntity : BaseEntity
{
    TEntity Add(TEntity Entity);
    void AddRange(TEntity[] Entities);
    //
    void Update(TEntity Entity);
    //
    void Delete(TEntityID Id);
    void DeleteRange(TEntityID[] Ids);
    //
    TEntity Get(TEntityID Id);
    TEntity Get(Expression<Func<TEntity, bool>> predicate);
    //
    IQueryable<TEntity> GetAll(int? skip, int? take);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
    //
    void Commit();
    void Rollback();
}

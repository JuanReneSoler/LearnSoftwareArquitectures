using System.Linq.Expressions;

namespace Domain.Base;

public interface IGenericRepository<TEntity, TEntityID>
    where TEntity : BaseEntity<TEntityID>
{
    void Add(TEntity Entity);
    //
    void Update(TEntity Entity);
    //
    void Delete(TEntityID Id);
    //
    TEntity? Get(TEntityID Id);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    //
    IQueryable<TEntity> GetAll(int? skip, int? take);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip, int? take);
    //
    bool Commit();
    void Rollback();
}

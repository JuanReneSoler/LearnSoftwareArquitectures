using Domain.Base;

namespace Domain.Repositories;

public interface IGenericRepository<TEntity> : IBaseRepository<TEntity, int>
    where TEntity : BaseEntity<int>
{
}

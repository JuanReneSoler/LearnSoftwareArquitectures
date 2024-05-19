using Domain.Models;

namespace Domain.Repositories;

public interface IGenericRepository<TEntity> : IBaseRepository<TEntity, int>
    where TEntity : BaseEntity<int>
{
}

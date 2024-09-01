using Domain.Entities;
using Domain.Repositories;

namespace Domain.UnitsOfWork;

public interface IGenericUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : BaseEntity<int>;
    Task<bool> Commit(CancellationToken cancellationToken);
    Task Rollback(CancellationToken cancellationToken);
}

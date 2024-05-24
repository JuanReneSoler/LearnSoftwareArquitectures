using Domain.Entities;
using Domain.Repositories;

namespace Domain.UnitsOfWork;

public interface IGenericUnitOfWork : IDisposable
{
    IGenericRepository<Tasks> Tasks { get; }
    IGenericRepository<Group> Groups { get; }
    IGenericRepository<Person> People { get; }
    Task<bool> Commit(CancellationToken cancellationToken);
    Task Rollback(CancellationToken cancellationToken);
}

using Domain.Entities;
using Domain.Repositories;

namespace Domain.UnitOfWork;

public interface IGenericUnitOfWork
{
    IGenericRepository<Tasks> Tasks { get; }
    IGenericRepository<Group> Groups { get; }
    IGenericRepository<Person> People { get; }
    Task<bool> Commit(CancellationToken cancellationToken);
    Task Rollback(CancellationToken cancellationToken);
}

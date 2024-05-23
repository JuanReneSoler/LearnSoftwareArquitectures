using Application.UnitOfWorks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public sealed class GenericUnitOfWork : IGenericUnitOfWork
{
    //
    public IGenericRepository<Tasks> Tasks => new GenericRepository<Tasks>(_context);

    public IGenericRepository<Group> Groups => new GenericRepository<Group>(_context);

    public IGenericRepository<Person> People => new GenericRepository<Person>(_context);

    private readonly SqlServerContext _context;

    public GenericUnitOfWork(SqlServerContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken) == 1;

    public async Task Rollback(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                    .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }, cancellationToken);
    }
}
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;
using Domain.UnitsOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public sealed class GenericUnitOfWork : IGenericUnitOfWork
{
    //
    public IGenericRepository<Tasks> Tasks => new GenericRepository<Tasks>(_context);

    public IGenericRepository<Group> Groups => new GenericRepository<Group>(_context);

    public IGenericRepository<Person> People => new GenericRepository<Person>(_context);

    private readonly SqlServerContext _context;
    private readonly IDomainEventDispatcher _dispatcher;

    public GenericUnitOfWork(
            SqlServerContext context,
            IDomainEventDispatcher Dispatcher)
    {
        _context = context;
        _dispatcher = Dispatcher;
    }

    public async Task<bool> Commit(CancellationToken cancellationToken)
    {
        await DispatchDomainEvents(cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) == 1;
    }

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

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            var domainEntities = _context.ChangeTracker
                .Entries<IGenericAgregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToArray();

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents)
                .ToArray();

            _dispatcher.Dispatch(domainEvents);
            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearEvents();
            }

        }, cancellationToken);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

using System.Linq.Expressions;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Contexts;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity<int>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public GenericRepository(SqlServerContext Context)
    {
        _context = Context;
        _table = Context.Set<TEntity>();
    }

    public async Task Add(TEntity Entity, CancellationToken cancellationToken) => await _table.AddAsync(Entity, cancellationToken);

    public async Task Update(TEntity Entity, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            _table.Update(Entity);
            _context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }, cancellationToken);
    }

    public async Task Delete(int Id, CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            var entity = (await this.Where(cancellationToken, x => x.Id == Id)).FirstOrDefault();

            if (entity is null) throw new NullReferenceException("El elemento no existe.");

            _table.Remove(entity);
        }, cancellationToken);
    }

    public async Task<IQueryable<TEntity>> Where(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate, int? skip = null, int? take = null)
    {
        return await Task.Run(() =>
        {
            var entities = _table.Where(predicate);

            if (skip != null && take != null)
                entities.Take(take.Value).Skip(skip.Value);

            return entities;
        }, cancellationToken);
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

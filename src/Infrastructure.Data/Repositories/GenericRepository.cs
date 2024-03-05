using System.Linq.Expressions;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity, int>
    where TEntity : BaseEntity<int>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public GenericRepository(SqlServerContext Context)
    {
        _context = Context;
        _table = Context.Set<TEntity>();
    }

    public void Add(TEntity Entity) => _table.Add(Entity);

    public bool Commit() => _context.SaveChanges() == 1;

    public void Delete(int Id)
    {
        var entity = this.Get(Id);

        if (entity is null) throw new NullReferenceException("El elemento no existe.");

        _table.Remove(entity);
    }

    public TEntity? Get(int Id)
    {
        var entity = _table.FirstOrDefault(x => x.Id == Id);
        return entity;
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = _table.FirstOrDefault(predicate);
        return entity;
    }

    public IQueryable<TEntity> GetAll(int? skip = null, int? take = null)
    {
        var items = _table.Select(x => x);

        if (skip != null && take != null)
            items = items.Skip(skip.Value).Take(take.Value);

        return items;
    }

    public void Rollback()
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
    }

    public void Update(TEntity Entity)
    {
        _table.Update(Entity);
        _context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip = null, int? take = null)
    {
        var entities = _table.Where(predicate);

        if (skip != null && take != null)
            entities.Take(take.Value).Skip(skip.Value);

        return entities;
    }
}

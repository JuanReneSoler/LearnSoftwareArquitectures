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

    public void Add(TEntity Entity) => _table.Add(Entity);

    public void Update(TEntity Entity)
    {
        _table.Update(Entity);
        _context.Entry(Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public void Delete(int Id)
    {
        var entity = this.Where(x => x.Id == Id).FirstOrDefault();

        if (entity is null) throw new NullReferenceException("El elemento no existe.");

        _table.Remove(entity);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip = null, int? take = null)
    {
        var entities = _table.Where(predicate);

        if (skip != null && take != null)
            entities.Take(take.Value).Skip(skip.Value);

        return entities;
    }

    public bool Commit() => _context.SaveChanges() == 1;

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
}

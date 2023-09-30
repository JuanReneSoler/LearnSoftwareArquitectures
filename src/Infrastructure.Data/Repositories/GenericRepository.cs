using System.Linq.Expressions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public GenericRepository(DbContext Context)
    {
        _context = Context;
        _table = Context.Set<TEntity>();
    }

    public TEntity Add(TEntity Entity)=>_table.Add(Entity).Entity;

    public void AddRange(TEntity[] Entities) => _table.AddRange(Entities);

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Delete(int Id)
    {
        var entity = this.Get(Id);

        if(entity is null) throw new NullReferenceException("El elemento no existe.");

        _table.Remove(entity);
    }

    public void DeleteRange(int[] Ids)
    {
        var entities = this.Where(x => Ids.Contains(x.Id));
    }

    public TEntity Get(int Id)
    {
        var entity = _table.FirstOrDefault(x=>x.Id == Id);

        if(entity is null) throw new NullReferenceException("El elemento no existe.");

        return entity;
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = _table.FirstOrDefault(predicate);

        if(entity is null) throw new NullReferenceException("El elemento no existe.");

        return entity;
    }

    public IList<TEntity> GetAll(int? skip = null, int? take = null)
    {
        var items = _table.Select(x=>x);

        if(!( ( skip ?? take ) is null ))
            items = items.Skip(skip.Value).Take(take.Value);

        return items.ToList();
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

    public IList<TEntity> Where(Expression<Func<TEntity, bool>> predicate, int? skip = null, int? take = null)
    {
        var entities = _table.Where(predicate);

        if(!((take ?? skip) is null))
            entities.Take(take.Value).Skip(skip.Value);

        return entities.ToList();
    }
}

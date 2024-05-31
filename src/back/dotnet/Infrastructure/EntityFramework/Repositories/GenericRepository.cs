using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using Domain.Entities;

namespace Infrastructure.EntityFramework;

public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity<int>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public GenericRepository(DbContext Context)
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
            _context.Entry(Entity).State = EntityState.Modified;
        }, cancellationToken);
    }

    public async Task Delete(int Id, CancellationToken cancellationToken)
    {
        if (await Exist(x => x.Id == Id, cancellationToken))
        {
            var entity = await Find(Id, cancellationToken);
            _table.Remove(entity);
        }
        throw new Exception($"No se encontro ningun elemento con el Id={Id}");
    }

    public async Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            return _table.Where(predicate);
        }, cancellationToken);
    }

    public async Task<bool> Exist(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        => await _table.AnyAsync(expression, cancellationToken);

    public async Task<TEntity> Find(int Id, CancellationToken cancellationToken)
    {
        if (await Exist(x => x.Id == Id, cancellationToken))
        {
            var result = await Where(x => x.Id == Id, cancellationToken);
            return result.First();
        }
        throw new Exception($"No se encontro ningun elemento con el Id={Id}");
    }
}

using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace SolarLab.Academy.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
{
    protected DbContext DbContext { get; }
    
    protected DbSet<TEntity> DbSet { get; }

    public Repository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }
    
    public  IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return DbSet.Where(predicate).AsNoTracking();
    }

    public ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        return  DbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity model, CancellationToken cancelationToken)
    {
        if (model == null)
        { 
            throw new ArgumentNullException(nameof(model));
        }

        await DbSet.AddAsync(model, cancelationToken);
        await DbContext.SaveChangesAsync(cancelationToken);
    }

    public async Task UpdateAsync(TEntity model, CancellationToken cancelationToken)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancelationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancelationToken)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancelationToken);
    }
}
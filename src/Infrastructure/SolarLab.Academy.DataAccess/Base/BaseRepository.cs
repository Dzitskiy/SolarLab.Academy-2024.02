using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Base;

namespace SolarLab.Academy.DataAccess.Base;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
{
    protected DbContext DbContext { get; }
    
    protected DbSet<TEntity> DbSet { get; }

    protected BaseRepository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> GetAll()
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
        return DbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        { 
            throw new ArgumentNullException(nameof(model));
        }

        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
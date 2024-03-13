using System.Linq.Expressions;
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

    public Task AddAsync(TEntity model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
using System.Linq.Expressions;

namespace SolarLab.Academy.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity: class
{
    /// <summary>
    /// Возвращает все элементы сущности <see cref="TEntity"/>
    /// </summary>
    /// <returns>Все элементы сущности <see cref="TEntity"/></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Возвращает все элементы сущности <see cref="TEntity"/> по предикату
    /// </summary>
    /// <param name="predicate">Предикат</param>
    /// <returns>все элементы сущности <see cref="TEntity"/> по предикату</returns>
    IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Возвращает элемент сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns><see cref="TEntity"/></returns>
    ValueTask<TEntity?> GetByIdAsync(Guid id);

    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
namespace SolarLab.Academy.Domain.Base;

/// <summary>
/// Базовый класс для всех сущностей.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания записи.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
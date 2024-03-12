using SolarLab.Academy.Domain.Base;

namespace SolarLab.Academy.Domain.Users.Entity;

/// <summary>
/// Пользователь.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string MiddleName { get; set; }
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDate { get; set; }
}
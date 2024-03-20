namespace SolarLab.Academy.Contracts.Users;

/// <summary>
/// Запрос на создание пользователя.
/// </summary>
public class CreateUserRequest
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
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Регион проживания.
    /// </summary>
    public int? Region { get; set; }
}
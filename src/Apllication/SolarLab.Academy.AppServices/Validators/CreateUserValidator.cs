using FluentValidation;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Validators;

/// <summary>
/// Валидатор запроса <see cref="CreateUserRequest"/>
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        // ФИО по длинне д.б. в установленных пределах по длине
        // NOTE ДЗ: 3 последних валидатора для ФИО одинаковые по смыслу - желательно оптимизировать
        RuleFor(x => x.FirstName).Length(1, 50).Matches(@"^[\p{L}]+$");
        RuleFor(x => x.LastName).Length(1, 50).Must(s => s.All(char.IsLetter));
        RuleFor(x => x.MiddleName).Length(1, 50).Must(s => !s.Any(c => !char.IsLetter(c)));

        // ДР должен быть указана и должна быть не менее 18 лет назад
        RuleFor(x => x.BirthDate).NotNull().LessThan(DateTime.Now.Date.AddYears(-18));

        // регион должен быть указан и быть в пределах (1-89)
        RuleFor(x => x.Region).NotNull().GreaterThan(0).LessThan(90);
    }
}
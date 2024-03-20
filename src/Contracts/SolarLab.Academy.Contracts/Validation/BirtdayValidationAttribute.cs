using SolarLab.Academy.Contracts.Users;
using System.ComponentModel.DataAnnotations;

namespace SolarLab.Academy.Contracts.Validation
{
    [Obsolete]
    public class BirtdayValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? dateTime = (DateTime?)value;
            return dateTime.HasValue && dateTime.Value < DateTime.Now.Date.AddYears(-18);
        }

        public override string FormatErrorMessage(string name)
        {
            if (name == nameof(CreateUserRequest.BirthDate))
            {
                return "Пользователь должен быть совершеннолетним.";
            }

            return base.FormatErrorMessage(name);
        }
    }
}

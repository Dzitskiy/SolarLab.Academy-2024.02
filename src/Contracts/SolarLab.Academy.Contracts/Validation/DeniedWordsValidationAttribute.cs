using System.ComponentModel.DataAnnotations;

namespace SolarLab.Academy.Contracts.Validation
{
    [Obsolete]
    public class DeniedWordsValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value.ToString().Contains("bla"))
            {
                return false;
            }

            return base.IsValid(value);
        }
    }
}

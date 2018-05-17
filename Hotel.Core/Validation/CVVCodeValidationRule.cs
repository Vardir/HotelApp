using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace HotelsApp.Core.Validation
{
    public class CVVCodeValidationRule : ValidationRule
    {
        const string CVV_REGEX = @"^[0-9]{2}-?[0-9]{2}$";

        public CVVCodeValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is string text) || text.Length == 0 || !Regex.IsMatch(text, CVV_REGEX))
                return new ValidationResult(false, "Incorrect CVV format");
            else
                return new ValidationResult(true, null);
        }

        public static bool IsValid(string text) => text != null && text.Length > 0 && Regex.IsMatch(text, CVV_REGEX);        
    }
}
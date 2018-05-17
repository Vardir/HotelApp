using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace HotelsApp.Core.Validation
{
    public class CreditCardCodeValidationRule : ValidationRule
    {
        const string CODE_REGEX = @"^[0-9]{4}-?[0-9]{4}-?[0-9]{4}-?[0-9]{4}$";

        public CreditCardCodeValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is string text) || text.Length == 0 || !Regex.IsMatch(text, CODE_REGEX))
                return new ValidationResult(false, "Incorrect credit card code format");
            else
                return new ValidationResult(true, null);
        }

        public static bool IsValid(string text) => text != null && text.Length > 0 && Regex.IsMatch(text, CODE_REGEX);        
    }
}
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace HotelsApp.Core.Validation
{
    public class NameValidationRule : ValidationRule
    {
        const string NAME_REGEX = @"^[\p{L}\s]+$";

        public NameValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is string text) || text.Length == 0 || !Regex.IsMatch((string)value, NAME_REGEX))
                return new ValidationResult(false, "Incorrect name format");
            else
                return new ValidationResult(true, null);
        }

        public static bool IsValid(string text) => text != null && text.Length > 0 && Regex.IsMatch(text, NAME_REGEX);        
    }
}
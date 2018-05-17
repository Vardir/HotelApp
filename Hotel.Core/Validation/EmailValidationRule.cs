using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace HotelsApp.Core.Validation
{
    public class EmailValidationRule : ValidationRule
    {
        const string EMAIL_REGEX = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";

        public EmailValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string text) || text.Length == 0 || !Regex.IsMatch(text, EMAIL_REGEX))
                return new ValidationResult(false, "Incorrect email format");
            else
                return new ValidationResult(true, null);
        }

        public static bool IsValid(string text) => text != null && text.Length > 0 && Regex.IsMatch(text, EMAIL_REGEX);        
    }
}
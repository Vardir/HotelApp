using System.Globalization;
using System.Windows.Controls;
using System.Windows;

namespace HotelsApp.Core.Validation
{
    public class StringValue : DependencyObject
    {
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(StringValue), new PropertyMetadata(null));
    }

    public class StringEqualityValidationRule : ValidationRule
    {
        public StringValue MainValue { get; set; }

        public StringEqualityValidationRule()
        {
            
        }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is string text) || value.Equals(MainValue.Value))
                return new ValidationResult(false, "Input value not matches");
            else
                return new ValidationResult(true, null);
        }

        public static bool IsValid(string main, string confirmation) => main != null && main.Equals(confirmation);
    }
}
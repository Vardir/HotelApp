using System;
using System.Windows;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class BoolToVisibilityConverter : BaseValueConverter<BoolToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                if (parameter is string param && param == "True")
                    return !v ? Visibility.Visible : Visibility.Collapsed;
                else return v ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

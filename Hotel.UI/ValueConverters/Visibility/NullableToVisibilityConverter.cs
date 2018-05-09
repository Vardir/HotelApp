using System;
using System.Windows;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class NullableToVisibilityConverter : BaseValueConverter<NullableToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

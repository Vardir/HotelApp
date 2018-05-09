using System;
using System.Windows;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class IntToVisibilityConverter : BaseValueConverter<IntToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v)
                return v != 0 ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

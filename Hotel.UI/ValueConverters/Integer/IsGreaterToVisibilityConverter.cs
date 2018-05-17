using System;
using System.Windows;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class IsGreaterToVisibilityConverter : BaseMultiValueConverter<IsGreaterToVisibilityConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                if (values[0] is int v1 && values[1] is int v2)
                    return v1 > v2 ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
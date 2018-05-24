using System;
using System.Windows;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class BoolToBrushConverter : BaseValueConverter<BoolToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
                return v ? Application.Current.FindResource("BackgroundGreenLightBrush") : Application.Current.FindResource("BackgroundRedLightBrush");
            return Application.Current.FindResource("BackgroundRedLightBrush");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
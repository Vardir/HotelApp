using System;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class DoubleToPercentConverter : BaseValueConverter<DoubleToPercentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v)
                return v >= 0 ? 100 * v : 0;
            return 0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
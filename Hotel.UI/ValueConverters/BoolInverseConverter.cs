using System;
using System.Globalization;

namespace Hotel.UI.ValueConverters
{
    public class BoolInverseConverter : BaseValueConverter<BoolInverseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
                return !v;
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
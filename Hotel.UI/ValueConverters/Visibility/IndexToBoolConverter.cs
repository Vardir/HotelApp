using System;
using System.Globalization;

namespace Hotel.UI.ValueConverters
{
    public class IndexToBoolConverter : BaseValueConverter<IndexToBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v)
                return v > -1;
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
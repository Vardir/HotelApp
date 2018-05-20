using System;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class NullableToBoolConverter : BaseValueConverter<NullableToBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
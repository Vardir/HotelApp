using System;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class FloatByPercentageConverter : BaseValueConverter<FloatByPercentageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is float percent)
            {
                if (percent > 0 && percent <= 1)
                {
                    return System.Convert.ToSingle(value) * percent;
                }
            }
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

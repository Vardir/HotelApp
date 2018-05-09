using System;
using System.Text;
using HotelsApp.UI.IoC;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class StarsToFontAwesomeConverter : BaseValueConverter<StarsToFontAwesomeConverter>
    {
        StringBuilder builder = new StringBuilder();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int starsCount)
            {
                string star = ResourceManager.GetSymbol("StarIcon");
                builder.Clear();
                for (int i = 0; i < starsCount; i++)
                    builder.Append(star);
                return builder.ToString();
            }
            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
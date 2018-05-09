using System;
using System.Globalization;

namespace HotelsApp.UI.ValueConverters
{
    public class RatingToTextConverter : BaseValueConverter<RatingToTextConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double rating)
            {
                if (rating < 3)
                    return "Very Bad";
                if (rating >= 3 && rating < 5)
                    return "Bad";
                if (rating >= 5 && rating < 7)
                    return "Normal";
                if (rating >= 7 && rating < 9)
                    return "Good";
                if (rating >= 9)
                    return "Very Good";
            }
            return "None";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
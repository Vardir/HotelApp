using System;
using HotelsApp.UI.Pages;
using System.Globalization;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.UI.ValueConverters.Page
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ApplicationPage page)
                return PageManager.Instance[page];
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
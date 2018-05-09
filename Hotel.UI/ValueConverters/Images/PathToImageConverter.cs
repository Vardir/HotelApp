using System;
using System.Globalization;
using System.IO;

namespace HotelsApp.UI.ValueConverters
{
    public class PathToImageConverter : BaseValueConverter<PathToImageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path && !string.IsNullOrWhiteSpace(path))
                return Path.GetFullPath($"Images/Uploads/{path}");
            return Path.GetFullPath("Images/Defaults/no-photo.png");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Windows;
using System.Globalization;
using HotelsApp.Core.DataModels;
using System.Collections.Generic;

namespace HotelsApp.UI.ValueConverters
{
    public class MessageTypeToIconConverter : BaseValueConverter<MessageTypeToIconConverter>
    {
        Dictionary<MessageType, string> keys = new Dictionary<MessageType, string>()
        {
            { MessageType.Text, "MessageIcon" },
            { MessageType.Warning, "WarningIcon" },
            { MessageType.Hint, "InfoIcon" },
            { MessageType.Error, "ErrorIcon" }
        };

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageType type)
                return Application.Current.FindResource(keys[type]);
            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
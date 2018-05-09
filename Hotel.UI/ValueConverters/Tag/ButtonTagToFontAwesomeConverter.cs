using System;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;

namespace HotelsApp.UI.ValueConverters
{
    public class ButtonTagToFontAwesomeConverter : BaseValueConverter<ButtonTagToFontAwesomeConverter>
    {
        static Dictionary<string, string> icons = new Dictionary<string, string>()
        {
            { "none", "StickyNoteIcon" },
            { "demark", "UngroupIcon" },

            { "open", "OpenIcon" },
            { "create", "CreateIcon" },
            { "refresh", "RefreshIcon" },
            { "book", "BookIcon" },
            { "light", "LightbulbIcon" },

            { "save", "SaveIcon" },
            { "revert", "RevertIcon" },
            { "back", "ArrowCircleLeftIcon" },

            { "add_square", "AddSquareIcon" },
            { "add_circle", "AddCircleIcon" },

            { "add_input", "InputIcon" },
            { "add_medium", "MediumIcon" },
            { "add_output", "OutputIcon" },

            { "remove", "RemoveIcon" },
            { "link", "LinkIcon" },
            { "unlink", "UnlinkIcon" },

        };

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string icon = icons["none"];
            if(value is string tag)
            {
                if (icons.TryGetValue(tag, out string v)) icon = v;
            }
            return Application.Current.FindResource(icon);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

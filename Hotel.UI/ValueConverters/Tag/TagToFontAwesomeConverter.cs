using System;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;

namespace HotelsApp.UI.ValueConverters
{
    public class TagToFontAwesomeConverter : BaseValueConverter<TagToFontAwesomeConverter>
    {
        static Dictionary<string, string> icons = new Dictionary<string, string>()
        {
            { "none", "StickyNoteIcon" },
            { "wifi", "WiFiIcon" },
            { "parking", "CarIcon" },
            { "family", "FamilyIcon" },
            { "disabled", "DisabledIcon" },
            { "no-smoking", "SmokingIcon" },
            { "restaurant", "RestaurantIcon" },
            { "24-hour", "ClockIcon" },
            { "cold", "SnowIcon" },
            { "pet", "PetIcon" },
            { "airport", "AirportIcon" },
            { "pool", "WaterIcon" },
            { "service", "ServiceIcon" },
            { "fitness", "FitnessIcon" },
            { "time", "ClockIcon" },
            { "spa", "BathIcon" },
            { "bath", "BathIcon" },
            { "safe", "SafeIcon" },
            { "cup", "CupIcon" },
            { "phone", "PhoneIcon" },
            { "tv", "TVIcon" },
            { "no-sound", "SoundOffIcon" },
            { "hot", "HotIcon" },
            { "balcony", "BuildingIcon" },
            { "view", "ViewIcon" },
            { "hairdryer", "HornIcon" },
            { "wardrobe", "BoxIcon" },
            { "elevator", "LevelUpIcon" },
            { "intercon", "ExchangeIcon" },
            { "bidet", "WaterIcon" },
        };

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string icon = icons["none"];
            if (value is string tag)
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
using System.ComponentModel;
using HotelsApp.Core.Converters;

namespace HotelsApp.Core.DataModels
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum HotelSortMode
    {
        [Description("Average price")]
        AvgPrice,
        [Description("Rating")]
        Rating,
        [Description("Stars")]
        Stars
    }
}
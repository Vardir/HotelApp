using System;

namespace HotelsApp.Core.Extensions
{
    public static class DateTimeHelpers
    {
        public static string ToSQL(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");
    }
}
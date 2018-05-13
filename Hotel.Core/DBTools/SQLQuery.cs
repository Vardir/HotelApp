using System;
using HotelsApp.Core.Extensions;

namespace HotelsApp.Core.DBTools
{
    public static class SQLQuery
    {
        public static string GetAllHotels() => "SELECT * FROM hotel";
        public static string GetHotelFacilities(int hotelId) => $"SELECT * FROM GetHotelFacilities({hotelId})";
        public static string GetHotelRoomTypes(int hotelId) => $"SELECT * FROM GetHotelRoomTypes({hotelId})";
        public static string GetRoomTypeFacilities(int roomTypeId) => $"SELECT * FROM GetRoomTypeFacilities({roomTypeId})";
        public static string GetAvailableRoomsForPeriod(int hotelId, int roomTypeId, DateTime start, DateTime end)
        {
            return string.Format("SELECT * FROM GetAvailableRoomsForPeriod({0}, {1}, CONVERT(date, '{2}'), CONVERT(date, '{3}'))",
                hotelId, roomTypeId, start.ToSQL(), end.ToSQL());
        }
    }
}
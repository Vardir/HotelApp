using System;
using HotelsApp.Core.DataModels;
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
            return string.Format("SELECT * FROM GetAvailableRoomsForPeriod({0}, {1}, '{2}', '{3}')",
                hotelId, roomTypeId, start.ToSQL(), end.ToSQL());
        }
        public static string RegisterCustomer(string name, string lastname, string email)
        {
            return string.Format("DECLARE @id int; exec @id = RegisterCustomer '{0}', '{1}', '{2}'; SELECT @id",
                                    name, lastname, email);
        }
        public static string MakeOrder(Order order)
        {
            return string.Format("DECLARE @res int; exec @res = MakeOrder {0}, {1}, {2}, '{3}', '{4}', {5}, {6}; SELECT @res",
                                  order.CustomerId, order.RoomTypeId, order.HotelId, 
                                  order.CheckInDate.ToSQL(), order.CheckOutDate.ToSQL(), order.TotalPrice, order.Fits);
        }
        public static string LoginAdmin(string name, string password) => $"SELECT dbo.LoginAdmin('{name}', '{password}')";
    }
}
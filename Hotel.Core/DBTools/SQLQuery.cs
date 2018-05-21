using System;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.Extensions;

namespace HotelsApp.Core.DBTools
{
    public static class SQLQuery
    {
        public static string GetAllHotels() => "SELECT * FROM GetHotels()";
        public static string GetHotel(int id) => $"SELECT * FROM hotel WHERE id = {id}";
        public static string GetHotelFacilities(int hotelId) => $"SELECT * FROM GetHotelFacilities({hotelId})";
        public static string GetHotelFacilitiesFlags(int hotelId) => $"SELECT * FROM GetHotelFacilities_Flags({hotelId})";
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
        public static string UpdateHotel(Hotel hotel)
        {
            return string.Format("UPDATE hotel SET Title='{1}', Stars={2}, Image='{3}', Description='{4}', Adress='{5}' WHERE id={0}", 
                hotel.Id, hotel.Title.Replace("'", "''"), hotel.Stars, hotel.Image, hotel.Description.Replace("'", "''"), hotel.Adress.Replace("'", "''"));
        }
        public static string LoginAdmin(string name, string password) => $"SELECT dbo.LoginAdmin('{name}', '{password}')";
    }
}
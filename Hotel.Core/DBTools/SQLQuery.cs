using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.Extensions;
using HotelsApp.Core.ViewModels.Items;

namespace HotelsApp.Core.DBTools
{
    public static class SQLQuery
    {
        static StringBuilder builder = new StringBuilder();

        public static string GetAllHotels() => "SELECT * FROM GetHotels()";
        public static string GetRoomTypes() => "SELECT * FROM room_type";
        public static string GetHotel(int id) => $"SELECT * FROM hotel WHERE id = {id}";
        public static string GetHotelRooms(int id) => $"SELECT * FROM room WHERE HotelId = {id}";
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
        public static string UpdatePrices(DataTable table, int hotelId)
        {
            builder.Clear();
            foreach (DataRow row in table.Rows)
            {                
                if (row.RowState == DataRowState.Modified)
                    builder.Append($"UPDATE room_type_price SET Price={ row["priceperfit"]} WHERE hotelid={hotelId} AND roomtypeid={row["id"]};");                
            }
            return builder.Length != 0 ? builder.ToString() : null;
        }
        public static string UpdateRooms(DataTable table, int hotelId)
        {
            builder.Clear();
            foreach (DataRow row in table.Rows)
            {
                if (row.HasErrors || row.RowState == DataRowState.Unchanged) continue;
                if (row.RowState == DataRowState.Deleted)
                {                    
                    builder.Append($"DELETE FROM room WHERE id={row["id", DataRowVersion.Original]};");
                    continue;
                }
                var id = row["id"];
                var code = row["roomcode"];
                var hotel = row["hotelid"] ?? hotelId;
                var type = row["typeid"];
                var locked = ToSqlBool(row["islocked"]);
                if (row.RowState == DataRowState.Added)
                    builder.Append($"INSERT INTO room (hotelid, typeid, islocked, roomcode) VALUES ({hotel}, {type}, {locked}, {code});");
                else if (row.RowState == DataRowState.Modified)
                    builder.Append($"UPDATE room SET islocked={locked}, roomcode={code}, typeid={type} WHERE id='{id}';");
            }
            return builder.Length != 0 ? builder.ToString() : null;
        }
        public static string UpdateFacilities(int hotelId, IEnumerable<FacilityViewModel> facilities)
        {
            builder.Clear();
            foreach (var facility in facilities)
            {
                if (facility.WasSelected && !facility.IsSelected)
                {
                    builder.Append($"DELETE FROM hotel_facility WHERE hotelid={hotelId} AND facilityid={facility.Id};");
                }
                else if (!facility.WasSelected && facility.IsSelected)
                {
                    builder.Append($"INSERT INTO hotel_facility VALUES({hotelId}, {facility.Id});");
                }
                facility.WasSelected = facility.IsSelected;
            }
            return builder.Length != 0 ? builder.ToString() : null;
        }
        public static string LoginAdmin(string name, string password) => $"SELECT dbo.LoginAdmin('{name}', '{password}')";

        static string ToSqlBool(object obj)
        {
            if (obj is int i)
                return i == 0 ? "0" : "1";
            if (obj is bool b)
                return b ? "1" : "0";
            return "0";
        }
    }
}
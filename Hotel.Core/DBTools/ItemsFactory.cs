using System.Data;
using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.DBTools
{
    public static class ItemsFactory
    {
        public static Facility GetFacility(DataRow row)
        {
            var extracted = Extract(row, "id", "title", "selected", "tag");            
            return new Facility
            {
                Id = Unbox<int>(extracted[0]),
                Title = Unbox<string>(extracted[1]),
                IsSelected = extracted[2] is int v && v > 0,
                Tag = Unbox<string>(extracted[3]),
            };
        }
        public static Hotel GetHotel(DataRow row)
        {
            var extracted = Extract(row, "id", "title", "adress", "image", "rating", "reviews", "stars", "description", "avgprice");
            return new Hotel
            {
                Id = Unbox<int>(extracted[0]),
                Title = Unbox<string>(extracted[1]),
                Adress = Unbox<string>(extracted[2]),
                Image = Unbox<string>(extracted[3]),
                Rating = Unbox<double>(extracted[4]),
                Reviews = Unbox<int>(extracted[5]),
                Stars = Unbox<byte>(extracted[6]),
                Description = Unbox<string>(extracted[7]),
                AvgPrice = Unbox<double>(extracted[8])
            };
        }
        public static RoomType GetRoomType(DataRow row)
        {
            var extracted = Extract(row, "id", "title", "description", "fits", "priceperfit", "needsprepay", "area");
            return new RoomType
            {
                Id = Unbox<int>(extracted[0]),
                Title = Unbox<string>(extracted[1]),
                Description = Unbox<string>(extracted[2]),
                Fits = Unbox<int>(extracted[3]),
                PricePerFit = Unbox<double>(extracted[4]),
                NeedsPrepay = Unbox<bool>(extracted[5]),
                Area = Unbox<double>(extracted[6])
            };
        }
        public static Facility GetOption(DataRow row)
        {
            var extracted = Extract(row, "id", "title", "tag");
            return new Facility
            {
                Id = Unbox<int>(extracted[0]),
                Title = Unbox<string>(extracted[1]),
                Tag = Unbox<string>(extracted[2])
            };
        }
        public static Room GetRoom(DataRow row)
        {
            var extracted = Extract(row, "id", "hotelid", "typeid", "islocked", "roomcode");
            return new Room
            {
                Id = Unbox<int>(extracted[0]),
                HotelId = Unbox<int>(extracted[1]),
                TypeId = Unbox<int>(extracted[2]),
                IsLocked = Unbox<bool>(extracted[3]),
                Code = Unbox<int>(extracted[4])
            };
        }

        static T Unbox<T>(object value)
        {
            if (value is T result)
                return result;
            return default(T);
        }
        static bool CanReceive(DataRow row, string column) => row.Table.Columns.Contains(column);
        static object[] Extract(DataRow row, params string[] columns)
        {
            object[] result = new object[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                if (CanReceive(row, columns[i]))
                    result[i] = row[columns[i]];
                else result[i] = null;
            }
            return result;
        }
    }
}
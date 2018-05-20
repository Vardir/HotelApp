using System;

namespace HotelsApp.Core.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public int Fits { get; set; }
        public int CustomerId { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public double TotalPrice { get; set; }
        public string Comments { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
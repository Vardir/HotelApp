using System;

namespace HotelsApp.Core.DataModels
{
    public class Order
    {
        public bool IsMoneyPayed { get; set; }
        public bool IsExpiered { get; set; }
        public int Id { get; set; }
        public int Fits { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
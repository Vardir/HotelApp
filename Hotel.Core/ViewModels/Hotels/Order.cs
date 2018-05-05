using System;

namespace Hotel.Core.ViewModels
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
        public DateTime DateOrdered { get; set; }
        public DateTime DatePayed { get; set; }
        public DateTime LivingDate { get; set; }
    }
}
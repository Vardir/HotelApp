namespace Hotel.Core.ViewModels
{
    public class Room
    {
        public bool IsEnabled { get; set; }
        public bool IsLocked { get; set; }
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Type { get; set; }
    }
}
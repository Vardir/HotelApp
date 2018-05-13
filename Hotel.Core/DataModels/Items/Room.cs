namespace HotelsApp.Core.DataModels
{
    public class Room
    {
        public bool IsLocked { get; set; }
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int TypeId { get; set; }
    }
}
namespace HotelsApp.Core.DataModels
{
    public class Hotel
    {
        public bool IsLocked { get; set; }
        public int Id { get; set; }
        public int Reviews { get; set; }
        public int Stars { get; set; }
        public double Budget { get; set; }
        public double AvgPrices { get; set; }
        public double AvailableRooms { get; set; }
        public double Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Image { get; set; }
    }
}
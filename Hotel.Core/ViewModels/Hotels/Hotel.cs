namespace Hotel.Core.ViewModels
{
    public class Hotel
    {
        public bool IsLocked { get; set; }
        public int Id { get; set; }
        public double Budget { get; set; }
        public double AvgPrices { get; set; }
        public double AvailableRooms { get; set; }
        public double Reputation { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
    }
}
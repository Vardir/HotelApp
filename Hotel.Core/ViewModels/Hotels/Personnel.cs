namespace Hotel.Core.ViewModels
{
    public class Personnel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int ActivitiesId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
    }
}
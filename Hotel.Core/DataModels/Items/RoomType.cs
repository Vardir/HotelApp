namespace HotelsApp.Core.DataModels
{
    public class RoomType
    {
        public bool NeedsPrepay { get; set; }
        public int Id { get; set; }
        public int Fits { get; set; }
        public double PricePerFit { get; set; }
        public double Area { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
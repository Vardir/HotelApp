using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.DesignModels
{
    public class HotelDesingModel : Hotel
    {
        static HotelDesingModel instance;
        public static HotelDesingModel Instance => instance ?? (instance = new HotelDesingModel());

        private HotelDesingModel()
        {
            Title = "Tourist Hotel Complex";
            Adress = "Dniprovskyj, Kiev";
            Reviews = 4747;
            Rating = 7.8;
            Stars = 3;
        }
    }
}
using HotelsApp.Core.ViewModels;

namespace HotelsApp.Core.DesignModels
{
    public class RoomTypeDesignModel : RoomTypeViewModel
    {
        static RoomTypeDesignModel instance;
        public static RoomTypeDesignModel Instance => instance ?? (instance = new RoomTypeDesignModel());

        private RoomTypeDesignModel()
        {
            Title = "Custom one type";
            Description = "A test one room type";
            PricePerFit = 200;
            Area = 15.2;
            Fits = 2;
            Options.Add(new DataModels.Option() { Content = "Option 1", Tag = "snow" });
            Options.Add(new DataModels.Option() { Content = "Option 2", Tag = "snow" });
            Options.Add(new DataModels.Option() { Content = "Option 3", Tag = "snow" });
        }
    }
}
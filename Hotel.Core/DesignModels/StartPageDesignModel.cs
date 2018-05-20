using HotelsApp.Core.DataModels;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.Core.DesignModels
{
    public class StartPageDesignModel : StartPageViewModel
    {
        static StartPageDesignModel instance;
        public static StartPageDesignModel Instance => instance ?? (instance = new StartPageDesignModel());

        private StartPageDesignModel()
        {
            hotelsList.Add(new HotelViewModel(new Hotel())
            {
                Title = "Generic hotel"
            });
        }
    }
}
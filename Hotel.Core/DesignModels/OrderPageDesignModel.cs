using HotelsApp.Core.ViewModels;

namespace HotelsApp.Core.DesignModels
{
    public class OrderPageDesignModel : OrderPageViewModel
    {
        static OrderPageDesignModel instance;
        public static OrderPageDesignModel Instance => instance ?? (instance = new OrderPageDesignModel());

        private OrderPageDesignModel()
        {

        }
    }
}
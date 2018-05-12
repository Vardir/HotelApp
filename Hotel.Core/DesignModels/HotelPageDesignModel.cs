using HotelsApp.Core.DataModels;
using HotelsApp.Core.Extensions;
using HotelsApp.Core.ViewModels;
using System.Collections.ObjectModel;

namespace HotelsApp.Core.DesignModels
{
    public class HotelPageDesignModel
    {
        static HotelPageDesignModel instance;
        public static HotelPageDesignModel Instance => instance ?? (instance = new HotelPageDesignModel());
        
        public HotelViewModel Hotel { get; }
        public ObservableCollection<Facility> Facilities { get; }
        public ObservableCollection<RoomTypeViewModel> RoomTypes { get; }

        private HotelPageDesignModel()
        {
            Hotel = new HotelViewModel
            {
                Title = HotelDesingModel.Instance.Title,
                Adress = HotelDesingModel.Instance.Adress,
                Reviews = HotelDesingModel.Instance.Reviews,
                Rating = HotelDesingModel.Instance.Rating,
                Stars = HotelDesingModel.Instance.Stars,
                Description = HotelDesingModel.Instance.Description,
                Image = "131133997.jpg"
            };
            Facilities = new ObservableCollection<Facility>
            {
                new Facility() {Title = "Facility 1", Tag = "snow"},
                new Facility() {Title = "Facility 2", Tag = "snow"},
                new Facility() {Title = "Facility 3", Tag = "snow"},
            };
            RoomTypes = new ObservableCollection<RoomTypeViewModel>
            {
                new RoomTypeViewModel(new RoomType()
                {
                    Title = "Room type 1", Description = "Description Description Description Description Description",
                    Area = 15.5, Fits = 2, NeedsPrepay = false, PricePerFit = 150
                }),
                new RoomTypeViewModel(new RoomType()
                {
                    Title = "Room type 2", Description = "Description Description Description Description Description",
                    Area = 15.5, Fits = 2, NeedsPrepay = false, PricePerFit = 150
                }),
                new RoomTypeViewModel(new RoomType()
                {
                    Title = "Room type 3", Description = "Description Description Description Description Description",
                    Area = 15.5, Fits = 2, NeedsPrepay = false, PricePerFit = 150
                }),
            };
            RoomTypes[0].Facilities.AddRange(new Facility[]
            {
                new Facility() { Title = "Option 1", Tag = "snow"},
                new Facility() { Title = "Option 2", Tag = "snow"},
                new Facility() { Title = "Option 3", Tag = "snow"},
                new Facility() { Title = "Option 4", Tag = "snow"},
                new Facility() { Title = "Option 5", Tag = "snow"},
                new Facility() { Title = "Option 6", Tag = "snow"},
                new Facility() { Title = "Option 7", Tag = "snow"},
            });
        }
    }
}
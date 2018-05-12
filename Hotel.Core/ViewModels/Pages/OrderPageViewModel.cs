using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DataModels;
using System.Collections.ObjectModel;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using HotelsApp.Core.ViewModels.Items;
using System;

namespace HotelsApp.Core.ViewModels
{    
    public class OrderPageViewModel : BasePageViewModel
    {
        HotelViewModel hotel;

        public HotelViewModel Hotel
        {
            get => hotel;
            set
            {
                if (hotel != value)
                {
                    hotel = value;
                    OnPropertyChanged(nameof(Hotel));
                }
            }
        }
        public OrderViewModel Order { get; }

        public OrderPageViewModel()
        {
            Order = new OrderViewModel
            {
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1),                
            };

        }
        
        protected override void InitializeCommands(){}

        public void Refresh()
        {
            
        }
    }
}
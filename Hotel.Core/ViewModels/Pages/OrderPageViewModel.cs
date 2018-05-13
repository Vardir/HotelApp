using System;
using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.ViewModels.Items;
using HotelsApp.Core.Extensions;

namespace HotelsApp.Core.ViewModels
{    
    public class OrderPageViewModel : BasePageViewModel
    {
        RoomTypeViewModel roomType;

        public RoomTypeViewModel RoomType
        {
            get => roomType;
            set
            {
                if (roomType != value)
                {
                    roomType = value;
                    OnPropertyChanged(nameof(RoomType));
                }
            }
        }
        public OrderViewModel Order { get; }
        
        public ObservableCollection<Room> Rooms { get; }

        public ICommand SearchCommand { get; set; }

        public OrderPageViewModel()
        {
            Rooms = new ObservableCollection<Room>();
            Order = new OrderViewModel
            {
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1),                
            };
        }
        
        protected override void InitializeCommands()
        {
            SearchCommand = new RelayCommand(SearchAvailableRooms);
        }

        public void Refresh()
        {
            
        }
        public void SearchAvailableRooms()
        {
            Rooms.Clear();
            string query = SQLQuery.GetAvailableRoomsForPeriod(RoomType.HotelId, RoomType.Id, Order.CheckInDate, Order.CheckOutDate);
            var dataSet = IoCContainer.Application.ExecuteQuery(query);
            if (dataSet.Tables.Count != 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    Rooms.Add(ItemsFactory.GetRoom(row));
                }
            }
        }
    }
}
using System;
using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.Validation;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.ViewModels.Items;

namespace HotelsApp.Core.ViewModels
{    
    public class OrderPageViewModel : BasePageViewModel
    {
        int rooms;
        string email;
        string confirmationEmail;
        string customerName;
        string customerLastname;
        RoomTypeViewModel roomType;

        public int RoomsOrdered
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {
                    rooms = value;                    
                    OnPropertyChanged(nameof(RoomsOrdered));
                }
            }
        }
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string ConfirmationEmail
        {
            get => confirmationEmail;
            set
            {
                if (confirmationEmail != value)
                {
                    confirmationEmail = value;
                    OnPropertyChanged(nameof(ConfirmationEmail));
                }
            }
        }
        public string CustomerName
        {
            get => customerName;
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }
        public string CustomerLastname
        {
            get => customerLastname;
            set
            {
                if (customerLastname != value)
                {
                    customerLastname = value;
                    OnPropertyChanged(nameof(CustomerLastname));
                }
            }
        }
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
        public ICommand ConfirmCommand { get; set; }

        public OrderPageViewModel()
        {
            Rooms = new ObservableCollection<Room>();
            Order = new OrderViewModel
            {
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1),                
            };
            Order.FitsChanged += Order_FitsChanged;
            Email = string.Empty;
        }

        void Order_FitsChanged(int count)
        {
            Order.TotalPrice = RoomType.PricePerFit * count;
            RoomsOrdered = (int)Math.Ceiling((double)count / (double)RoomType.Fits);
        }

        protected override void InitializeCommands()
        {
            SearchCommand = new RelayCommand(SearchAvailableRooms);
            ConfirmCommand = new RelayCommand(Confirm);
        }

        public void Confirm()
        {
            if (IsValid())
            {

            }
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
                Order.UpdateFits(Order.Fits);
            }
        }

        public bool IsValid()
        {
            bool value = NameValidationRule.IsValid(CustomerName);
            value &= NameValidationRule.IsValid(CustomerLastname);
            value &= EmailValidationRule.IsValid(Email);
            value &= StringEqualityValidationRule.IsValid(Email, ConfirmationEmail);
            value &= Rooms.Count >= RoomsOrdered;
            value &= Rooms.Count > 0;
            return value;
        }
    }
}
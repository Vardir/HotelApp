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
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{    
    public class OrderPageViewModel : BasePageViewModel
    {
        bool suppressChecks;
        int rooms;
        string email;
        string confirmationEmail;
        string customerName;
        string customerLastname;
        string cvvCode;
        string cardCode;
        RoomTypeViewModel roomType;
        
        public string CVV
        {
            get => cvvCode;
            set
            {
                if (cvvCode != value)
                {
                    cvvCode = value;
                    OnPropertyChanged(nameof(CVV));
                }
            }
        }
        public string CardCode
        {
            get => cardCode;
            set
            {
                if (cardCode != value)
                {
                    cardCode = value;
                    OnPropertyChanged(nameof(CardCode));
                }
            }
        }
        public int RoomsOrdered
        {
            get => rooms;
            set
            {
                if (rooms != value || suppressChecks)
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
                if (email != value || suppressChecks)
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
                if (confirmationEmail != value || suppressChecks)
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
                if (customerName != value || suppressChecks)
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
                if (customerLastname != value || suppressChecks)
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
        public ICommand GoBackCommand { get; set; }

        public OrderPageViewModel()
        {
            Rooms = new ObservableCollection<Room>();
            Order = new OrderViewModel();
            Order.FitsChanged += Order_FitsChanged;
            Refresh();
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
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.HotelPage, null);
        }
        public void Confirm()
        {
            if (IsValid())
            {

            }
        }
        public void Refresh()
        {
            suppressChecks = true;
            CustomerName = null;
            CustomerLastname = null;
            Email = null;
            ConfirmationEmail = null;
            CVV = null;//"00-00";
            CardCode = null;//"0000-0000-0000-0000";
            suppressChecks = false;
            Rooms.Clear();
            Order.Clear();
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
            if (roomType.NeedsPrepay)
            {
                value &= CVVCodeValidationRule.IsValid(CVV);
                value &= CreditCardCodeValidationRule.IsValid(CardCode);
            }
            return value;
        }
    }
}
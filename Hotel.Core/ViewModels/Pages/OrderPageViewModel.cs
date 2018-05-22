using System;
using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.Validation;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.DataModels.Page;
using HotelsApp.Core.ViewModels.Items;
using HotelsApp.Core.ViewModels.Dialogs;

namespace HotelsApp.Core.ViewModels
{    
    public class OrderPageViewModel : BasePageViewModel
    {
        #region Private Fields
        bool suppressChecks;
        int rooms;
        string email;
        string confirmationEmail;
        string customerName;
        string customerLastname;
        string cvvCode;
        string cardCode;
        DateTime? lastCheckIn;
        DateTime? lastCheckOut;
        RoomTypeViewModel roomType; 
        #endregion

        #region Public Props
        public bool RoomsSearched => lastCheckIn != null && lastCheckOut != null;
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
        #endregion

        #region Commands
        public ICommand SearchCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        #endregion

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
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.HotelPage, null);
        }
        public void Confirm()
        {
            if (IsValid())
            {
                string query = SQLQuery.RegisterCustomer(customerName, customerLastname, email);
                int id = IoCContainer.Application.ExecuteScalarQuery<int>(query, out string error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
                if (id == -1)
                {
                    IoCContainer.Application.ShowMessage("There is already registered user with such an email. Make sure your credentials is valid", MessageType.Warning);
                    return;
                }
                Order.HotelId = RoomType.HotelId;
                Order.CustomerId = id;
                Order.RoomTypeId = RoomType.Id;
                query = SQLQuery.MakeOrder(Order.GetRawData());
                int responce = IoCContainer.Application.ExecuteScalarQuery<int>(query, out error);
                if (error != null)
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                else if (responce == 0)
                    IoCContainer.Application.ShowMessage("We can not verify your order. Try search rooms again or check your credentials", MessageType.Warning);
                else
                {
                    IoCContainer.UI.ShowMessage(new MessageBoxDialogViewModel() { Message = "Your order confirmed!", Title = "Success" });
                    IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
                }
            }
            else IoCContainer.Application.ShowMessage("Verify your credentials again", MessageType.Warning);
        }
        public void Refresh()
        {
            suppressChecks = true;
            lastCheckIn = null;
            lastCheckOut = null;
            OnPropertyChanged(nameof(RoomsSearched));
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
            lastCheckIn = Order.CheckInDate;
            lastCheckOut = Order.CheckOutDate;
            OnPropertyChanged(nameof(RoomsSearched));

            string query = SQLQuery.GetAvailableRoomsForPeriod(RoomType.HotelId, RoomType.Id, Order.CheckInDate, Order.CheckOutDate);
            var dataTable = IoCContainer.Application.ExecuteTableQuery(query, out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Rooms.Add(ItemsFactory.GetRoom(row));
            }
            Order.UpdateFits(Order.Fits);
        } 
        #endregion

        public bool IsValid()
        {
            bool value = RoomsSearched;
            value &= NameValidationRule.IsValid(CustomerName);
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
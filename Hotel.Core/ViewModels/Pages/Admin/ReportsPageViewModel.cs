using System;
using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class ReportsPageViewModel : BasePageViewModel
    {
        bool applyFilter;
        int totalCount;
        int totalFits;
        double totalPrice;
        double availableRooms;
        DateTime month;
        HotelViewModel hotel;
        DataTable ordersSummary;
        DataTable dailyRoomsSummary;
        
        public bool ApplyFilter
        {
            get => applyFilter;
            set
            {
                if (applyFilter != value)
                {
                    applyFilter = value;
                    FilterAvailable(value);
                    OnPropertyChanged(nameof(ApplyFilter));
                }
            }
        }
        public int TotalCount
        {
            get => totalCount;
            set
            {
                if (totalCount != value)
                {
                    totalCount = value;
                    OnPropertyChanged(nameof(TotalCount));
                }
            }
        }
        public int TotalFits
        {
            get => totalFits;
            set
            {
                if (totalFits != value)
                {
                    totalFits = value;
                    OnPropertyChanged(nameof(TotalFits));
                }
            }
        }
        public double TotalPrice
        {
            get => totalPrice;
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        public double AvailableRooms
        {
            get => availableRooms;
            set
            {
                if (availableRooms != value)
                {
                    availableRooms = value;
                    OnPropertyChanged(nameof(AvailableRooms));
                }
            }
        }
        public DateTime Month
        {
            get => month;
            set
            {
                if (month != value)
                {
                    month = value;
                    LoadOrders();
                    OnPropertyChanged(nameof(Month));
                }
            }
        }
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
        public DataTable OrdersSummary
        {
            get => ordersSummary;
            set
            {
                if (ordersSummary != value)
                {
                    ordersSummary = value;
                    OnPropertyChanged(nameof(OrdersSummary));
                }
            }
        }
        public DataTable DailyRoomsSummary
        {
            get => dailyRoomsSummary;
            set
            {
                if (dailyRoomsSummary != value)
                {
                    dailyRoomsSummary = value;
                    OnPropertyChanged(nameof(DailyRoomsSummary));
                }
            }
        }

        public ICommand FilterCommand { get; set; }

        public event Action ReportLoaded;

        public ReportsPageViewModel()
        {
            var today = DateTime.Today;
            Month = new DateTime(today.Year, today.Month, 1);
        }
        
        protected override void InitializeCommands()
        {
            FilterCommand = new RelayCommand(() => ApplyFilter = !ApplyFilter);
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.HotelEditPage, Hotel);
        }
        public void Refresh()
        {
            if (hotel == null) return;

            DataTable dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRoomsOnDate(Hotel.Id, DateTime.Now), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            DailyRoomsSummary = dataTable;
            ApplyFilter = true;

            double count = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.Field<object>("RoomId") is int)
                    count++;
            }
            AvailableRooms = 1 - ((count * 100.0) / dataTable.Rows.Count / 100);

            LoadOrders();

            ReportLoaded?.Invoke();
        }
        public void LoadOrders()
        {
            if (Hotel == null) return;

            DataTable dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelOrdersOnPeriod(Hotel.Id, Month, Month.AddMonths(1)), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            OrdersSummary = dataTable;
            TotalCount = OrdersSummary.Rows.Count;

            int fits = 0;
            double price = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                fits += row.Field<int>("Fits");
                price += row.Field<double>("Price");
            }
            TotalFits = fits;
            TotalPrice = price;
        }
        #endregion

        void FilterAvailable(bool apply)
        {
            if (DailyRoomsSummary?.DefaultView == null) return;
            if (apply)
            {
                DailyRoomsSummary.DefaultView.RowFilter = "RoomId IS NULL";
            }
            else DailyRoomsSummary.DefaultView.RowFilter = "RoomId IS NOT NULL";
        }
    }
}
using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Data;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using System.Collections.Generic;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using System.ComponentModel;

namespace HotelsApp.Core.ViewModels
{
    public class StartPageViewModel : BasePageViewModel
    {
        HotelSortMode sortingMode;
        ListSortDirection sortDirection;

        protected List<HotelViewModel> hotelsList;

        public ListSortDirection SortDirection
        {
            get => sortDirection;
            set
            {
                if (sortDirection != value)
                {
                    sortDirection = value;
                    Sort(SortingMode);
                    OnPropertyChanged(nameof(SortDirection));
                }
            }
        }
        public HotelSortMode SortingMode
        {
            get => sortingMode;
            set
            {
                if (sortingMode != value)
                {
                    sortingMode = value;
                    Sort(value);
                    OnPropertyChanged(nameof(SortingMode));
                }
            }
        }
        public CollectionViewSource Hotels { get; }

        public ICommand LoginCommand { get; set; }

        public StartPageViewModel()
        {
            hotelsList = new List<HotelViewModel>();
            Hotels = new CollectionViewSource
            {
                Source = hotelsList
            };
        }
        
        protected override void InitializeCommands()
        {
            LoginCommand = new RelayCommand(GoToLogin);
        }

        public void GoToLogin()
        {
            IoCContainer.Application.GoTo(ApplicationPage.LoginPage, null);
        }
        public void Refresh()
        {
            hotelsList.Clear();
            var dataSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetAllHotels(), out string _);
            if (dataSet.Tables.Count != 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    hotelsList.Add(new HotelViewModel(ItemsFactory.GetHotel(row))
                    {
                        SelectCommand = new RelayParameterizedCommand(SelectHotel)
                    });
                }
            }
            SortingMode = HotelSortMode.Rating;
        }
        public void SelectHotel(object obj)
        {
            if (obj is HotelViewModel hotel)
            {
                IoCContainer.Application.GoTo(ApplicationPage.HotelPage, hotel);
            }
        }
        public void Sort(HotelSortMode sortMode)
        {
            SetSortDescription(sortMode.ToString(), SortDirection);
        }

        void SetSortDescription(string prop, ListSortDirection sortDirection)
        {
            Hotels.SortDescriptions.Clear();
            Hotels.SortDescriptions.Add(new SortDescription(prop, sortDirection));
        }
    }
}
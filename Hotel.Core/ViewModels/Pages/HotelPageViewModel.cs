using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DataModels;
using System.Collections.ObjectModel;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using System.Windows.Input;

namespace HotelsApp.Core.ViewModels
{    
    public class HotelPageViewModel : BasePageViewModel
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
        public ObservableCollection<Facility> Facilities { get; }
        public ObservableCollection<RoomTypeViewModel> RoomTypes { get; }

        public ICommand GoBackCommand { get; set; }

        public HotelPageViewModel()
        {
            Facilities = new ObservableCollection<Facility>();
            RoomTypes = new ObservableCollection<RoomTypeViewModel>();
        }
        
        protected override void InitializeCommands()
        {
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void Refresh()
        {
            Facilities.Clear();
            RoomTypes.Clear();
            var dataSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelFacilities(Hotel.Id), out string _);
            if (dataSet.Tables.Count == 1)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    Facilities.Add(ItemsFactory.GetFacility(row));
                }
            }
            dataSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRoomTypes(Hotel.Id), out string _);
            if (dataSet.Tables.Count == 1)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    var roomType = new RoomTypeViewModel(ItemsFactory.GetRoomType(row))
                    {
                        HotelId = Hotel.Id,
                        ReserveCommand = new RelayParameterizedCommand(Reserve)
                    };
                    RoomTypes.Add(roomType);
                    var facilitiesSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetRoomTypeFacilities(roomType.Id), out string _);
                    if (facilitiesSet.Tables.Count == 1)
                    {
                        var facilitiesTable = facilitiesSet.Tables[0];
                        foreach (DataRow facilityRow in facilitiesTable.Rows)
                        {
                            roomType.Facilities.Add(ItemsFactory.GetOption(facilityRow));
                        }
                    }
                }
            }
        }
        public void Reserve(object obj)
        {
            if (obj is RoomTypeViewModel model)
            {
                IoCContainer.Application.GoTo(ApplicationPage.OrderPage, model, TransitionOptions.KeepData);
            }
        }
    }
}
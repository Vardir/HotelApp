using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.DataModels.Page;

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
        
        public ICommand LoginCommand { get; set; }

        public HotelPageViewModel()
        {
            Facilities = new ObservableCollection<Facility>();
            RoomTypes = new ObservableCollection<RoomTypeViewModel>();
        }
        
        protected override void InitializeCommands()
        {            
            LoginCommand = new RelayCommand(GoToLogin);
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void GoToLogin()
        {
            IoCContainer.Application.GoTo(ApplicationPage.LoginPage, null);
        }
        public void Refresh()
        {
            if (Hotel == null) return;

            Facilities.Clear();
            RoomTypes.Clear();
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelFacilities(Hotel.Id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                Facilities.Add(ItemsFactory.GetFacility(row));
            }
            dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRoomTypes(Hotel.Id), out error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                var roomType = new RoomTypeViewModel(ItemsFactory.GetRoomType(row))
                {
                    HotelId = Hotel.Id,
                    ReserveCommand = new RelayParameterizedCommand(Reserve)
                };
                RoomTypes.Add(roomType);
                var facilitiesTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetRoomTypeFacilities(roomType.Id), out error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }                
                foreach (DataRow facilityRow in facilitiesTable.Rows)
                {
                    roomType.Facilities.Add(ItemsFactory.GetOption(facilityRow));
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
        #endregion
    }
}
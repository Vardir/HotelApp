using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DBTools;
using System.Collections.ObjectModel;
using HotelsApp.Core.ViewModels.Items;
using System.Windows.Input;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using HotelsApp.Core.ViewModels.Dialogs;
using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.ViewModels
{
    public class HotelEditPageViewModel : BasePageViewModel
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
        public ObservableCollection<FacilityViewModel> Facilities { get; }

        public ICommand SaveCommand { get; set; }
        public ICommand ManageRoomsCommand { get; set; }

        public HotelEditPageViewModel()
        {
            Facilities = new ObservableCollection<FacilityViewModel>();
        }
        
        protected override void InitializeCommands()
        {
            SaveCommand = new RelayCommand(Save);
            ManageRoomsCommand = new RelayCommand(ManageRooms);
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void ManageRooms()
        {
            IoCContainer.Application.GoTo(ApplicationPage.RoomsManagerPage, Hotel);
        }
        public void Refresh()
        {
            if (hotel == null) return;
            Facilities.Clear();
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelFacilitiesFlags(Hotel.Id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            foreach (DataRow row in dataTable.Rows)
            {
                Facilities.Add(new FacilityViewModel(ItemsFactory.GetFacility(row)));
            }            
        }
        public void Save()
        {
            var query = SQLQuery.UpdateHotel(Hotel.GetInternalData());
            IoCContainer.Application.ExecuteTableQuery(query, out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            query = SQLQuery.UpdateFacilities(Hotel.Id, Facilities);
            if (query != null)
            {
                IoCContainer.Application.ExecuteTableQuery(query, out error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
            }
            IoCContainer.Application.ShowMessage("Changes committed successfully");
        } 
        #endregion
    }
}
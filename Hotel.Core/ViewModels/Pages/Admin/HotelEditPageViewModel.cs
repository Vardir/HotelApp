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

        public ICommand GoBackCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public HotelEditPageViewModel()
        {
            Facilities = new ObservableCollection<FacilityViewModel>();
        }
        
        protected override void InitializeCommands()
        {
            GoBackCommand = new RelayCommand(GoBack);
            SaveCommand = new RelayCommand(Save);
        }

        public void Refresh()
        {
            if (hotel == null) return;
            Facilities.Clear();
            var dataSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelFacilitiesFlags(Hotel.Id), out string _);
            if (dataSet.Tables.Count != 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    Facilities.Add(new FacilityViewModel(ItemsFactory.GetFacility(row)));
                }
            }
        }
        public void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void Save()
        {
            var query = SQLQuery.UpdateHotel(Hotel.GetInternalData());
            IoCContainer.Application.ExecuteTableQuery(query, out string error);
            if (error != null)
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
            else
                IoCContainer.Application.ShowMessage("Changes committed successfully");            
        }
    }
}
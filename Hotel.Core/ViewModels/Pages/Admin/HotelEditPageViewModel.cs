using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DBTools;
using System.Collections.ObjectModel;
using HotelsApp.Core.ViewModels.Items;

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

        public HotelEditPageViewModel()
        {
            Facilities = new ObservableCollection<FacilityViewModel>();
        }
        
        protected override void InitializeCommands()
        {
            
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
    }
}
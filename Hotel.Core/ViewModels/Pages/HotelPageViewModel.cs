using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DataModels;
using System.Collections.ObjectModel;
using HotelsApp.Core.DBTools;

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

        public HotelPageViewModel()
        {
            Facilities = new ObservableCollection<Facility>();
            RoomTypes = new ObservableCollection<RoomTypeViewModel>();
        }
        
        protected override void InitializeCommands()
        {
        }

        public void Refresh()
        {
            Facilities.Clear();
            var dataSet = IoCContainer.Application.ExecuteQuery($"SELECT * FROM GetHotelFacilities({Hotel.Id})");
            if (dataSet.Tables.Count == 1)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    Facilities.Add(ItemsFactory.GetFacility(row));
                }
            }
            dataSet = IoCContainer.Application.ExecuteQuery($"SELECT * FROM GetHotelRoomTypes({Hotel.Id})");
            if (dataSet.Tables.Count == 1)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    var roomType = new RoomTypeViewModel(ItemsFactory.GetRoomType(row));
                    RoomTypes.Add(roomType);
                    var optionsSet = IoCContainer.Application.ExecuteQuery($"SELECT * FROM GetRoomTypeOptions({roomType.Id})");
                    if (optionsSet.Tables.Count == 1)
                    {
                        var optionsTable = optionsSet.Tables[0];
                        foreach (DataRow optionRow in optionsTable.Rows)
                        {
                            roomType.Options.Add(ItemsFactory.GetOption(optionRow));
                        }
                    }
                }
            }
        }
    }
}
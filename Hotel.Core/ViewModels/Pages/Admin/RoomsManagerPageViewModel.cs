using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class RoomsManagerPageViewModel : BasePageViewModel
    {
        DataTable rooms;
        DataTable roomTypes;
        HotelViewModel hotel;

        public DataTable Rooms
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged(nameof(Rooms));
                }
            }
        }
        public DataTable RoomTypes
        {
            get => roomTypes;
            set
            {
                if (roomTypes != value)
                {
                    roomTypes = value;
                    OnPropertyChanged(nameof(RoomTypes));
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

        public ICommand SaveCommand { get; set; }

        public RoomsManagerPageViewModel()
        {
            
        }
        
        protected override void InitializeCommands()
        {
            SaveCommand = new RelayCommand(Save);
        }

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.HotelEditPage, Hotel);
        }
        public void Refresh()
        {
            if (hotel == null) return;
            
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRoomTypes(Hotel.Id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            RoomTypes = dataTable;

            dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRooms(Hotel.Id), out error);            
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            Rooms = dataTable;
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
        public void DeleteRoom(object obj)
        {
            if (obj is int index)
            {
                var row = rooms.Rows[index];
                rooms.DefaultView.Delete(index);
                row.Delete();
            }
        }
        public void Insert()
        {
            if (Rooms != null)
            {
                Rooms.Rows.Add(Rooms.NewRow());
            }
        }
        #endregion
    }
}
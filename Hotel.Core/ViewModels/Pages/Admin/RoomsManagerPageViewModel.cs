using System.Data;
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using System.Collections.Generic;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class RoomsManagerPageViewModel : BasePageViewModel
    {
        bool applyFilter;
        int selectedRoomType;
        DataTable rooms;
        DataTable roomPrices;
        HotelViewModel hotel;

        public bool ApplyFilter
        {
            get => applyFilter;
            set
            {
                if (applyFilter != value)
                {
                    applyFilter = value;
                    Filter(value);
                    OnPropertyChanged(nameof(ApplyFilter));
                }
            }
        }
        public int SelectedRoomType
        {
            get => selectedRoomType;
            set
            {
                if (selectedRoomType != value)
                {
                    selectedRoomType = value;
                    Filter(ApplyFilter);
                    OnPropertyChanged(nameof(SelectedRoomType));
                }
            }
        }
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
        public DataTable RoomPrices
        {
            get => roomPrices;
            set
            {
                if (roomPrices != value)
                {
                    roomPrices = value;
                    OnPropertyChanged(nameof(RoomPrices));
                }
            }
        }
        public List<RoomType> RoomTypes { get; }
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
        public ICommand InsertRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        public RoomsManagerPageViewModel()
        {
            RoomTypes = new List<RoomType>();
        }
        
        protected override void InitializeCommands()
        {
            SaveCommand = new RelayCommand(Save);
            InsertRoomCommand = new RelayCommand(InsertRoom);
            DeleteRoomCommand = new RelayParameterizedCommand(DeleteRoom);
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
            
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotelRoomTypes(Hotel.Id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            RoomPrices = dataTable;

            dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetRoomTypes(), out error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            RoomTypes.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                RoomTypes.Add(ItemsFactory.GetRoomType(row));
            }
            if (RoomTypes.Count > 0)
                SelectedRoomType = RoomTypes[0].Id;

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
            string error = null;
            bool commited = false;
            var query = SQLQuery.UpdatePrices(RoomPrices, Hotel.Id);
            if (query != null)
            {
                IoCContainer.Application.ExecuteTableQuery(query, out error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
                commited = true;
            }
            query = SQLQuery.UpdateRooms(Rooms, Hotel.Id);
            if (query != null)
            {
                IoCContainer.Application.ExecuteTableQuery(query, out error);
                if (error != null)
                {
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
                commited = true;
            }
            if (commited)
            {
                IoCContainer.Application.ShowMessage("Changes committed successfully");
                Refresh();
            }
            else IoCContainer.Application.ShowMessage("No changes was made");
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
        public void InsertRoom()
        {
            if (Rooms != null)
            {
                var row = Rooms.NewRow();
                row["hotelid"] = Hotel.Id;
                row["typeid"] = RoomTypes[0].Id;
                row["roomcode"] = Rooms.DefaultView.Count != 0 ? ((int)Rooms.DefaultView[Rooms.DefaultView.Count - 1]["roomcode"]) + 1 : 1;
                row["islocked"] = false;
                Rooms.Rows.Add(row);
            }
        }
        #endregion

        void Filter(bool apply)
        {
            if (Rooms?.DefaultView == null) return;
            if (apply)
            {
                Rooms.DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                Rooms.DefaultView.RowFilter = $"TypeId = {selectedRoomType}";
            }
            else Rooms.DefaultView.RowFilter = string.Empty;
        }
    }
}
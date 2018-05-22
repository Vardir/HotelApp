using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.ViewModels
{
    public class RoomViewModel : BaseViewModel
    {
        Room actualData;
       
        public bool IsLocked
        {
            get => actualData.IsLocked;
            set
            {
                if (actualData.IsLocked != value)
                {
                    actualData.IsLocked = value;
                    OnPropertyChanged(nameof(IsLocked));
                }
            }
        }
        public int Id => actualData.Id;
        public int Code
        {
            get => actualData.Code;
            set
            {
                if (actualData.Code != value)
                {
                    actualData.Code = value;
                    OnPropertyChanged(nameof(Code));
                }
            }
        }
        public int HotelId => actualData.HotelId;
        public int TypeId
        {
            get => actualData.TypeId;
            set
            {
                if (actualData.TypeId != value)
                {
                    actualData.TypeId = value;
                    OnPropertyChanged(nameof(TypeId));
                }
            }
        }

        public RoomViewModel(Room type = null)
        {
            actualData = type ?? new Room();
        }
    }
}
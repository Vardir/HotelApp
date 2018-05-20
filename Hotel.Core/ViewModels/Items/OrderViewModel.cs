using System;
using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.ViewModels.Items
{
    public class OrderViewModel : BaseViewModel
    {
        int days;
        Order actualData;

        public int Id
        {
            get => actualData.Id;
            set
            {
                if (actualData.Id != value)
                {
                    actualData.Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public int Fits
        {
            get => actualData.Fits;
            set
            {
                if (actualData.Fits != value)
                {
                    FitsChanged?.Invoke(value);
                    actualData.Fits = value;
                    OnPropertyChanged(nameof(Fits));
                }
            }
        }
        public int CustomerId
        {
            get => actualData.CustomerId;
            set
            {
                if (actualData.CustomerId != value)
                {
                    actualData.CustomerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                }
            }
        }
        public int HotelId
        {
            get => actualData.HotelId;
            set
            {
                if (actualData.HotelId != value)
                {
                    actualData.HotelId = value;
                    OnPropertyChanged(nameof(HotelId));
                }
            }
        }
        public int RoomTypeId
        {
            get => actualData.RoomTypeId;
            set
            {
                if (actualData.RoomTypeId != value)
                {
                    actualData.RoomTypeId = value;
                    OnPropertyChanged(nameof(RoomTypeId));
                }
            }
        }
        public int Days
        {
            get => days;
            private set
            {
                if (days != value)
                {
                    days = value;
                    OnPropertyChanged(nameof(Days));
                }
            }
        }
        public double TotalPrice
        {
            get => actualData.TotalPrice;
            set
            {
                if (actualData.TotalPrice != value)
                {
                    actualData.TotalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        public DateTime CheckInDate
        {
            get => actualData.CheckInDate;
            set
            {
                if (actualData.CheckInDate != value)
                {
                    actualData.CheckInDate = value;
                    if (value >= actualData.CheckOutDate)
                        CheckOutDate = value.AddDays(1);
                    Days = CheckOutDate.Subtract(CheckInDate).Days;
                    OnPropertyChanged(nameof(CheckInDate));
                }
            }
        }
        public DateTime CheckOutDate
        {
            get => actualData.CheckOutDate;
            set
            {
                if (actualData.CheckOutDate != value && actualData.CheckInDate < value)
                {
                    actualData.CheckOutDate = value;
                    Days = CheckOutDate.Subtract(CheckInDate).Days;
                    OnPropertyChanged(nameof(CheckOutDate));
                }
            }
        }

        public event Action<int> FitsChanged;

        public OrderViewModel(Order order = null)
        {
            actualData = order ?? new Order();
            Clear();
        }

        public void UpdateFits(int value)
        {
            Fits = value;
            OnPropertyChanged(nameof(Fits));
            FitsChanged?.Invoke(value);
        }
        public void Clear()
        {
            Fits = 1;
            CustomerId = 0;
            RoomTypeId = 0;
            Days = 1;
            TotalPrice = 0;
            CheckInDate = DateTime.Today;
            CheckOutDate = CheckInDate.AddDays(1);
        }

        public Order GetRawData()
        {
            return new Order()
            {
                Id = Id, Fits = Fits, CheckInDate = CheckInDate, CheckOutDate = CheckOutDate,
                Comments = actualData.Comments, CustomerId = CustomerId, 
                HotelId = HotelId, RoomTypeId = RoomTypeId, TotalPrice = TotalPrice
            };
        }
    }
}
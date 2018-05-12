using System;
using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.ViewModels.Items
{
    public class OrderViewModel : BaseViewModel
    {
        Order actualData;

        public bool IsMoneyPayed
        {
            get => actualData.IsMoneyPayed;
            set
            {
                if (actualData.IsMoneyPayed != value)
                {
                    actualData.IsMoneyPayed = value;
                    OnPropertyChanged(nameof(IsMoneyPayed));
                }
            }
        }
        public bool IsExpiered
        {
            get => actualData.IsExpiered;
            set
            {
                if (actualData.IsExpiered != value)
                {
                    actualData.IsExpiered = value;
                    OnPropertyChanged(nameof(IsExpiered));
                }
            }
        }
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
        public int RoomId
        {
            get => actualData.RoomId;
            set
            {
                if (actualData.RoomId != value)
                {
                    actualData.RoomId = value;
                    OnPropertyChanged(nameof(RoomId));
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
                    OnPropertyChanged(nameof(CheckOutDate));
                }
            }
        }

        public OrderViewModel(Order order = null)
        {
            actualData = order ?? new Order();
        }
    }
}
using System;
using System.Windows.Input;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;

namespace HotelsApp.Core.ViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        Hotel actualData;

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
        public int Reviews
        {
            get => actualData.Reviews;
            set
            {
                if (actualData.Reviews != value)
                {
                    actualData.Reviews = value;
                    OnPropertyChanged(nameof(Reviews));
                }
            }
        }
        public int Stars
        {
            get => actualData.Stars;
            set
            {
                if (actualData.Stars != value)
                {
                    actualData.Stars = value;
                    OnPropertyChanged(nameof(Stars));
                }
            }
        }
        public double Budget
        {
            get => actualData.Budget;
            set
            {
                if (actualData.Budget != value)
                {
                    actualData.Budget = value;
                    OnPropertyChanged(nameof(Budget));
                }
            }
        }
        public double AvgPrices
        {
            get => actualData.AvgPrice;
            set
            {
                if (actualData.AvgPrice != value)
                {
                    actualData.AvgPrice = value;
                    OnPropertyChanged(nameof(AvgPrices));
                }
            }
        }
        public double AvailableRooms
        {
            get => actualData.AvailableRooms;
            set
            {
                if (actualData.AvailableRooms != value)
                {
                    actualData.AvailableRooms = value;
                    OnPropertyChanged(nameof(AvailableRooms));
                }
            }
        }
        public double Rating
        {
            get => actualData.Rating;
            set
            {
                if (actualData.Rating != value)
                {
                    actualData.Rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }
        public string Title
        {
            get => actualData.Title;
            set
            {
                if (actualData.Title != value)
                {
                    actualData.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Description
        {
            get => actualData.Description;
            set
            {
                if (actualData.Description != value)
                {
                    actualData.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public string Adress
        {
            get => actualData.Adress;
            set
            {
                if (actualData.Adress != value)
                {
                    actualData.Adress = value;
                    OnPropertyChanged(nameof(Adress));
                }
            }
        }
        public string Image
        {
            get => actualData.Image;
            set
            {
                if (actualData.Image != value)
                {
                    actualData.Image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public ICommand SelectCommand { get; set; }

        public HotelViewModel(Hotel data = null)
        {
            actualData = data ?? new Hotel();
        }
    }
}
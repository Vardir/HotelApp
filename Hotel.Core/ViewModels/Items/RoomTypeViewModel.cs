﻿using System.Windows.Input;
using HotelsApp.Core.DataModels;
using System.Collections.ObjectModel;

namespace HotelsApp.Core.ViewModels
{
    public class RoomTypeViewModel : BaseViewModel
    {
        RoomType actualData;

        public bool NeedsPrepay
        {
            get => actualData.NeedsPrepay;
            set
            {
                if (actualData.NeedsPrepay != value)
                {
                    actualData.NeedsPrepay = value;
                    OnPropertyChanged(nameof(NeedsPrepay));
                }
            }
        }
        public int Id => actualData.Id;
        public int HotelId { get; set; }
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
        public double PricePerFit
        {
            get => actualData.PricePerFit;
            set
            {
                if (actualData.PricePerFit != value)
                {
                    actualData.PricePerFit = value;
                    OnPropertyChanged(nameof(PricePerFit));
                }
            }
        }
        public double Area
        {
            get => actualData.Area;
            set
            {
                if (actualData.Area != value)
                {
                    actualData.Area = value;
                    OnPropertyChanged(nameof(Area));
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

        public ObservableCollection<Facility> Facilities { get; }

        public ICommand ReserveCommand { get; set; }

        public RoomTypeViewModel(RoomType type = null)
        {
            actualData = type ?? new RoomType();
            Facilities = new ObservableCollection<Facility>();
        }
    }
}
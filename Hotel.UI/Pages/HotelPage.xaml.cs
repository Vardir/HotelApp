﻿using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for HotelPage.xaml
    /// </summary>
    public partial class HotelPage : BasePage<HotelPageViewModel>
    {
        public HotelPage()
        {
            InitializeComponent();
            Title = "Start page";
            PageContextChanged += (value) =>
            {
                if (value is HotelViewModel hotel)
                {
                    ViewModel.Hotel = hotel;
                    ViewModel.Refresh();
                }
            };
            Loaded += HotelPage_Loaded;
        }

        private void HotelPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ViewModel != null)
                ViewModel.Refresh();
        }
    }
}
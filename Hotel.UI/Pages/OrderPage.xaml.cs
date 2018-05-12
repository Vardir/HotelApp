using System;
using System.Windows;
using System.Windows.Controls;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class OrderPage : BasePage<OrderPageViewModel>
    {
        public OrderPage()
        {
            InitializeComponent();
            Title = "Order page";
            Loaded += StartupPage_Loaded;
            checkInDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            checkOutDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today));
        }

        void StartupPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
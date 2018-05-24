using System.Windows;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class HotelEditPage : BasePage<HotelEditPageViewModel>
    {
        public HotelEditPage()
        {
            InitializeComponent();
            Loaded += StartupPage_Loaded;
            PageContextChanged += (context) =>
            {
                if (context is HotelViewModel hotel)
                {
                    ViewModel.Hotel = hotel;
                    ViewModel.Refresh();
                }
            };
        }

        void StartupPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
using System.Windows;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class StartupPage : BasePage<StartPageViewModel>
    {
        public StartupPage()
        {
            InitializeComponent();
            Title = "Start page";
            Loaded += StartupPage_Loaded;            
        }

        void StartupPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
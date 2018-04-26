using Hotel.Core.ViewModels;

namespace Hotel.UI.Pages
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
        }
    }
}
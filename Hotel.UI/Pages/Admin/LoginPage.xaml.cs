using System.Security;
using System.Windows;
using HotelsApp.Core.Security;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginPageViewModel>, IHaveSecureString
    {
        public SecureString SecureString => password.SecurePassword;

        public LoginPage()
        {
            InitializeComponent();
            Title = "Login page";
            Loaded += StartupPage_Loaded;            
        }

        void StartupPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
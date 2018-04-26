using Hotel.UI.IoC;
using Hotel.Core.IoC;
using System.Windows;
using Hotel.Core.IoC.Interfaces;

namespace Hotel.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationSetup();
            var window = new MainWindow();
            Current.MainWindow = window;
            window.Show();
        }

        void ApplicationSetup()
        {
            IoCContainer.SetUp();
            IoCContainer.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }
    }
}

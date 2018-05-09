using HotelsApp.UI.IoC;
using HotelsApp.Core.IoC;
using System.Windows;
using HotelsApp.Core.IoC.Interfaces;

namespace HotelsApp.UI
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

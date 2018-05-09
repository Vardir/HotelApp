using System.Windows;
using HotelsApp.UI.ViewModels;

namespace HotelsApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel(this);
            ViewModelLocator.Application.ReadConfig("config.xml");
        }

        void Window_Deactivated(object sender, System.EventArgs e)
        {
            (DataContext as WindowViewModel).DimmableOverlayVisible = true;
        }
        void Window_Activated(object sender, System.EventArgs e)
        {
            (DataContext as WindowViewModel).DimmableOverlayVisible = false;
        }
    }
}

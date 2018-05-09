using System.Windows;
using HotelsApp.UI.ViewModels;

namespace HotelsApp.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        DialogWindowViewModel viewModel;

        public DialogWindowViewModel ViewModel
        {
            get => viewModel;
            set
            {
                if (value == null) return;
                viewModel = value;
                DataContext = viewModel;
            }
        }

        public DialogWindow()
        {
            InitializeComponent();
        }
    }
}

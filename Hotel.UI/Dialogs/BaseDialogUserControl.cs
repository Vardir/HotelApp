using System.Windows;
using Hotel.UI.ViewModels;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hotel.Core.RelayCommands;
using Hotel.Core.ViewModels.Dialogs;

namespace Hotel.UI.Dialogs
{
    public class BaseDialogUserControl : UserControl
    {
        DialogWindow dialogWindow;

        public int WindowMinimumWidth { get; set; } = 250;
        public int WindowMinimumHeight { get; set; } = 100;
        public int TitleHeight { get; set; } = 30;
        public string Title { get; set; }

        public ICommand CloseCommand { get; set; }

        public BaseDialogUserControl()
        {
            dialogWindow = new DialogWindow();
            dialogWindow.ViewModel = new DialogWindowViewModel(dialogWindow);

            CloseCommand = new RelayCommand(dialogWindow.Close);
        }

        public Task ShowDialog<T>(T viewModel)
            where T : BaseDialogViewModel
        {
            var tcs = new TaskCompletionSource<bool>();
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    dialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    dialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    dialogWindow.ViewModel.TitleHight = TitleHeight;
                    dialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    dialogWindow.ViewModel.Content = this;
                    DataContext = viewModel;

                    dialogWindow.ShowDialog();
                }
                finally
                {
                    tcs.SetResult(true);
                }
            });
            return tcs.Task;
        }
    }
}
using System.Windows;
using System.Windows.Controls;

namespace HotelsApp.UI.ViewModels
{
    public class DialogWindowViewModel : WindowViewModel
    {
        Control contentControl;

        public Control Content
        {
            get => contentControl;
            set
            {
                if (contentControl != value)
                {
                    contentControl = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        public DialogWindowViewModel(Window window) : base(window)
        {
            WindowMinimumHeight = 100;
            WindowMinimumWidth = 250;
            TitleHight = 30;
        }
    }
}
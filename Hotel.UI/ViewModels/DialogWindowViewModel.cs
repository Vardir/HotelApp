using System.Windows;
using System.Windows.Controls;

namespace Hotel.UI.ViewModels
{
    public class DialogWindowViewModel : WindowViewModel
    {
        string title;
        Control contentControl;

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
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
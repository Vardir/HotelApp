namespace HotelsApp.Core.ViewModels.Dialogs
{
    public class MessageBoxDialogViewModel : BaseDialogViewModel
    {
        string message;
        string okText;

        public string Message
        {
            get => message;
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }
        public string OkText
        {
            get => okText;
            set
            {
                if (okText != value)
                {
                    okText = value;
                    OnPropertyChanged(nameof(OkText));
                }
            }
        }

        public MessageBoxDialogViewModel()
        {
            OkText = "Ok";
        }
    }
}
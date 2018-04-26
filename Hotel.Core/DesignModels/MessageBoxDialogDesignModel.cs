using Hotel.Core.ViewModels.Dialogs;

namespace Hotel.Core.DesignModels
{
    public class MessageBoxDialogDesignModel : MessageBoxDialogViewModel
    {
        static MessageBoxDialogDesignModel instance;
        public static MessageBoxDialogDesignModel Instance => instance ?? (instance = new MessageBoxDialogDesignModel());

        private MessageBoxDialogDesignModel()
        {
            Title = "Message";
            Message = "Hello!";
            OkText = "Ok";
        }
    }
}

namespace HotelsApp.Core.ViewModels.Dialogs
{
    public class BaseDialogViewModel : BaseViewModel
    {
        string title;

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
    }
}
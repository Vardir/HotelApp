using Hotel.Core.DataModels.Page;

namespace Hotel.Core.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        ApplicationPage currentPage;
        TransitionOptions currentTransitionOptions;
        BaseViewModel currentPageContext;

        public ApplicationPage CurrentPage
        {
            get => currentPage;
            private set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public TransitionOptions CurrentTransitionOptions
        {
            get => currentTransitionOptions;
            set
            {
                if (currentTransitionOptions != value)
                {
                    currentTransitionOptions = value;
                    OnPropertyChanged(nameof(CurrentTransitionOptions));
                }
            }
        }
        public BaseViewModel CurrentPageContext
        {
            get => currentPageContext;
            set
            {
                if (currentPageContext != value)
                {
                    currentPageContext = value;
                    OnPropertyChanged(nameof(CurrentPageContext));
                }
            }
        }

        public ApplicationViewModel()
        {
            GoTo(ApplicationPage.StartPage, null);
        }

        public void GoTo(ApplicationPage page, BaseViewModel viewModel, TransitionOptions options = TransitionOptions.ClearData)
        {
            CurrentPage = page;
            CurrentTransitionOptions = options;
            CurrentPageContext = viewModel;
        }
    }
}
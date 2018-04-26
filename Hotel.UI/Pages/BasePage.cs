using System;
using Hotel.Core.ViewModels;
using System.Windows.Controls;

namespace Hotel.UI.Pages
{
    public abstract class BasePage : Page
    {
        object pageContext;

        public int Level { get; set; }
        public abstract object PageViewModel { get; }
        public object PageContext
        {
            get => pageContext;
            set
            {
                if (pageContext != value)
                {
                    pageContext = value;
                    PageContextChanged?.Invoke(value);
                }
            }
        }

        public event Action<object> PageContextChanged;

        public BasePage()
        {
            KeepAlive = true;
        }
    }

    public class BasePage<TViewModel> : BasePage
        where TViewModel : BaseViewModel, new()
    {
        TViewModel viewModel;

        public TViewModel ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel == value) return;
                viewModel = value;
                DataContext = viewModel;
            }
        }
        public override object PageViewModel => ViewModel;

        public BasePage() : base()
        {
            ViewModel = new TViewModel();
        }
    }
}
using HotelsApp.UI.Pages;
using System.Windows;
using HotelsApp.Core.ViewModels;
using System.Windows.Controls;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.UI.Controls
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        public BaseViewModel PageContext
        {
            get => (BaseViewModel)GetValue(PageContextProperty);
            set => SetValue(PageContextProperty, value);
        }
        public ApplicationPage CurrentPage
        {
            get => (ApplicationPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        #region Dependency Properties
        public static readonly DependencyProperty PageContextProperty =
            DependencyProperty.Register(nameof(PageContext), typeof(BaseViewModel), typeof(PageHost),
                new UIPropertyMetadata(null, null, PageContextChanged));

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(ApplicationPage), typeof(PageHost),
                new UIPropertyMetadata(default(ApplicationPage), null, CurrentPageChanged));
        #endregion

        public PageHost()
        {
            InitializeComponent();
        }

        static object PageContextChanged(DependencyObject d, object value)
        {
            var host = d as PageHost;
            var frameContent = host.PageFrame.Content;
            if (frameContent is BasePage basePage)
            {
                basePage.PageContext = value;
            }
            return value;
        }
        static object CurrentPageChanged(DependencyObject d, object value)
        {
            var host = d as PageHost;
            var frameContent = host.PageFrame.Content;
            var appPage = (ApplicationPage)value;
            if (frameContent == null ||
                PageManager.Instance.BasePageType(frameContent as BasePage) != appPage)
            {
                host.DataContext = PageManager.Instance[appPage];
                host.PageFrame.ContentRendered += (s, e) =>
                {
                    if (((Frame)s).Content is BasePage page)
                        page.PageContext = host.PageContext;
                };
            }
            return value;
        }
    }
}

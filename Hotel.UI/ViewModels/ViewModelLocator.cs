using HotelsApp.Core.IoC;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator instance = new ViewModelLocator();
        public static ViewModelLocator Instance => instance;

        public static ApplicationViewModel Application => IoCContainer.Application;
    }
}
using Hotel.Core.IoC;
using Hotel.Core.ViewModels;

namespace Hotel.UI.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator instance = new ViewModelLocator();
        public static ViewModelLocator Instance => instance;

        public static ApplicationViewModel Application => IoCContainer.Application;
    }
}
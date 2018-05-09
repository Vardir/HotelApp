using Ninject;
using HotelsApp.Core.ViewModels;
using HotelsApp.Core.IoC.Interfaces;

namespace HotelsApp.Core.IoC
{
    public static class IoCContainer
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();
        public static IUIManager UI => Get<IUIManager>();
        public static ApplicationViewModel Application => Get<ApplicationViewModel>();

        public static void SetUp()
        {
            BindViewModels();
        }

        static void BindViewModels()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        public static T Get<T>() => Kernel.Get<T>();
    }
}
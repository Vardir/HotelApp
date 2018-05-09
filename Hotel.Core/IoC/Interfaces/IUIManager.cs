using System.Threading.Tasks;
using HotelsApp.Core.ViewModels.Dialogs;

namespace HotelsApp.Core.IoC.Interfaces
{
    public interface IUIManager
    {
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
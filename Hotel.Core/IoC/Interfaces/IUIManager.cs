using System.Threading.Tasks;
using Hotel.Core.ViewModels.Dialogs;

namespace Hotel.Core.IoC.Interfaces
{
    public interface IUIManager
    {
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
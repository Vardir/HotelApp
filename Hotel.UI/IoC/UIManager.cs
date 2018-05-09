using HotelsApp.UI.Dialogs;
using System.Threading.Tasks;
using HotelsApp.Core.IoC.Interfaces;
using HotelsApp.Core.ViewModels.Dialogs;

namespace HotelsApp.UI.IoC
{
    public class UIManager : IUIManager
    {        
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }
    }
}
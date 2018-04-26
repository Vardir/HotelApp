using Hotel.UI.Dialogs;
using System.Threading.Tasks;
using Hotel.Core.IoC.Interfaces;
using Hotel.Core.ViewModels.Dialogs;

namespace Hotel.UI.IoC
{
    public class UIManager : IUIManager
    {        
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }
    }
}
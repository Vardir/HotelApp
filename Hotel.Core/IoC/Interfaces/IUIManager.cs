using System.IO;
using System.Threading.Tasks;
using HotelsApp.Core.ViewModels.Dialogs;

namespace HotelsApp.Core.IoC.Interfaces
{
    public interface IUIManager
    {
        bool SaveImage(string path);
        FileInfo LoadFile(string filter);
        Task ShowMessage(MessageBoxDialogViewModel viewModel);
    }
}
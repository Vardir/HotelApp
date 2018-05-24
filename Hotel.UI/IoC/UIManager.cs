using System.IO;
using HotelsApp.UI.Dialogs;
using System.Threading.Tasks;
using HotelsApp.Core.IoC.Interfaces;
using HotelsApp.Core.ViewModels.Dialogs;
using System.Windows.Forms;
using System;

namespace HotelsApp.UI.IoC
{
    public class UIManager : IUIManager
    {
        OpenFileDialog openFileDialog;

        public UIManager()
        {
            openFileDialog = new OpenFileDialog
            {
                AddExtension = true
            };
        }

        public FileInfo LoadFile(string filter)
        {
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return new FileInfo(openFileDialog.FileName);
            }
            return null;
        }
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }

        public bool SaveImage(string path)
        {
            if (File.Exists(path))
            {
                var dest = AppDomain.CurrentDomain.BaseDirectory + @"Images\Uploads\" + Path.GetFileName(path);
                File.Copy(path, dest, true);
                return true;
            }
            return false;
        }
    }
}
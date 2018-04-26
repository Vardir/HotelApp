using System;
using Hotel.Core.Expressions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Hotel.Core.ViewModels
{
    [Serializable]
    public class BaseViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(BaseViewModel model, string propName)
        {
            PropertyChanged?.Invoke(model, new PropertyChangedEventArgs(propName));
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) then the action is not run.
        /// If the flag is false (indicating no function is run) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">Flag defining if the command is already running</param>
        /// <param name="command">Command to run</param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> command)
        {
            if (updatingFlag.GetPropertyValue()) return;
            updatingFlag.SetPropertyValue(true);

            try
            {
                await command();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}

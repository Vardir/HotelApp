using System;
using System.Windows.Input;

namespace HotelsApp.Core.RelayCommands
{
    public class RelayParameterizedCommand : ICommand
    {
        Action<object> action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayParameterizedCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
﻿using System;
using System.Windows.Input;

namespace HotelsApp.Core.RelayCommands
{
    public class RelayCommand : ICommand
    {
        Action action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
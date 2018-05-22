using System.Windows.Input;
using System.Collections.Generic;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public abstract class BasePageViewModel : BaseViewModel
    {
        string name;
        List<PageCommand> commands;
        
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public List<PageCommand> Commands
        {
            get => commands;
            set
            {
                if (commands != value)
                {
                    commands = value;
                    OnPropertyChanged(nameof(Commands));
                }
            }
        }

        public ICommand GoBackCommand { get; set; }

        public BasePageViewModel()
        {
            Commands = new List<PageCommand>();
            InitializeCommands();
            GoBackCommand = new RelayCommand(GoBack);
        }

        abstract protected void InitializeCommands();
        public abstract void GoBack();
    }
}
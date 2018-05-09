using HotelsApp.Core.DataModels.Page;
using System.Collections.Generic;

namespace HotelsApp.Core.ViewModels
{
    public class BasePageViewModel : BaseViewModel
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

        public BasePageViewModel()
        {
            Commands = new List<PageCommand>();
            InitializeCommands();
        }

        virtual protected void InitializeCommands() { }
    }
}
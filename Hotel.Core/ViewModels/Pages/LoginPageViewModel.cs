﻿using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.Security;
using HotelsApp.Core.Extensions;
using HotelsApp.Core.RelayCommands;

namespace HotelsApp.Core.ViewModels
{
    public class LoginPageViewModel : BasePageViewModel
    {
        string message;
        string username;

        public string Message
        {
            get => message;
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(){}
        
        protected override void InitializeCommands()
        {
            LoginCommand = new RelayParameterizedCommand(Login);
        }

        public void Refresh()
        {
            Message = null;
            Username = null;
        }
        public void Login(object param)
        {
            if (param is IHaveSecureString container)
            {
                string query = SQLQuery.LoginAdmin(Username, container.SecureString.Unsecure().GetMD5());
                int responce = IoCContainer.Application.ExecuteScalarQuery<int>(query, out string error);
                if (error != null)
                {
                    Message = error;
                    return;
                }
                switch (responce)
                {
                    case -2:
                        Message = "Invalid username"; break;
                    case -1:
                        Message = "Invalid password"; break;
                    default:
                        IoCContainer.UI.ShowMessage(new Dialogs.MessageBoxDialogViewModel() { Message = "Logged in" });
                        break;
                }
            }
        }
    }
}
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.Security;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.Extensions;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class LoginPageViewModel : BasePageViewModel
    {
        string username;

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

        #region Actions
        public override void GoBack()
        {
            IoCContainer.Application.GoTo(ApplicationPage.StartPage, null);
        }
        public void Refresh()
        {
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
                    IoCContainer.Application.ShowMessage(error, MessageType.Error);
                    return;
                }
                switch (responce)
                {
                    case -2:
                        IoCContainer.Application.ShowMessage("Invalid username", MessageType.Warning); break;
                    case -1:
                        IoCContainer.Application.ShowMessage("Invalid password", MessageType.Warning); break;
                    default:
                        OpenHotel(responce);
                        break;
                }
            }
        } 
        #endregion

        void OpenHotel(int id)
        {
            var dataTable = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotel(id), out string error);
            if (error != null)
            {
                IoCContainer.Application.ShowMessage(error, MessageType.Error);
                return;
            }
            if (dataTable.Rows.Count == 1)
            {
                var hotel = new HotelViewModel(ItemsFactory.GetHotel(dataTable.Rows[0]));
                IoCContainer.Application.GoTo(ApplicationPage.HotelEditPage, hotel);
            }
            else
                IoCContainer.Application.ShowMessage("An error occurred while retrieving data from server. Please, try again or contact administration", MessageType.Error);
        }
    }
}
using HotelsApp.Core.IoC;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.Security;
using HotelsApp.Core.Extensions;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using HotelsApp.Core.DataModels;

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
        public ICommand GoBackCommand { get; set; }

        public LoginPageViewModel(){}
        
        protected override void InitializeCommands()
        {
            LoginCommand = new RelayParameterizedCommand(Login);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void GoBack()
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
        void OpenHotel(int id)
        {
            var dataSet = IoCContainer.Application.ExecuteTableQuery(SQLQuery.GetHotel(id), out string _);
            if (dataSet.Tables.Count != 0)
            {
                var table = dataSet.Tables[0];
                if (table.Rows.Count == 1)
                {
                    var hotel = new HotelViewModel(ItemsFactory.GetHotel(table.Rows[0]));
                    IoCContainer.Application.GoTo(ApplicationPage.HotelEditPage, hotel);
                }
            }
        }
    }
}
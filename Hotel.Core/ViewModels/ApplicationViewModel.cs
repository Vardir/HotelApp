using System;
using System.Data;
using System.Threading;
using System.Windows.Input;
using System.Threading.Tasks;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        bool showMessageBox;
        bool pinMessageBox;
        MessageType messageType;
        ApplicationPage currentPage;
        TransitionOptions currentTransitionOptions;
        string messageText;
        SqlAdapter dbAdapter;
        BaseViewModel currentPageContext;

        public bool ShowMessageBox
        {
            get => showMessageBox;
            private set
            {
                if (showMessageBox != value)
                {
                    showMessageBox = value;
                    OnPropertyChanged(nameof(ShowMessageBox));
                }
            }
        }
        public MessageType MessageType
        {
            get => messageType;
            set
            {
                if (messageType != value)
                {
                    messageType = value;
                    OnPropertyChanged(nameof(MessageType));
                }
            }
        }
        public ApplicationPage CurrentPage
        {
            get => currentPage;
            private set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public TransitionOptions CurrentTransitionOptions
        {
            get => currentTransitionOptions;
            set
            {
                if (currentTransitionOptions != value)
                {
                    currentTransitionOptions = value;
                    OnPropertyChanged(nameof(CurrentTransitionOptions));
                }
            }
        }
        public string MessageText
        {
            get => messageText;
            private set
            {
                if (messageText != value)
                {
                    messageText = value;
                    OnPropertyChanged(nameof(MessageText));
                }
            }
        }
        public BaseViewModel CurrentPageContext
        {
            get => currentPageContext;
            set
            {
                if (currentPageContext != value)
                {
                    currentPageContext = value;
                    OnPropertyChanged(nameof(CurrentPageContext));
                }
            }
        }

        public ICommand CloseMessageBoxCommand { get; set; }
        public ICommand PinMessageBoxCommand { get; set; }

        public ApplicationViewModel()
        {
            GoTo(ApplicationPage.StartPage, null);
            CloseMessageBoxCommand = new RelayCommand(CloseMessageBox);
            PinMessageBoxCommand = new RelayCommand(PinMessageBox);
        }

        public void PinMessageBox() => pinMessageBox = !pinMessageBox;
        public void CloseMessageBox() => ShowMessageBox = false;
        public void ShowMessage(string text, MessageType messageType = MessageType.Text)
        {
            pinMessageBox = false;
            MessageText = text;
            ShowMessageBox = true;
            MessageType = messageType;
            if (MessageType != MessageType.Error)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    if (!pinMessageBox) ShowMessageBox = false;
                    pinMessageBox = false;
                });
            }
            else pinMessageBox = false;
        }
        public void GoTo(ApplicationPage page, BaseViewModel viewModel, TransitionOptions options = TransitionOptions.ClearData)
        {
            CurrentPage = page;
            CurrentTransitionOptions = options;
            CurrentPageContext = viewModel;
            CloseMessageBox();
        }
        public void ReadConfig(string path)
        {
            dbAdapter = new SqlAdapter(new ConnectionInfo(path));
        }

        public DataSet ExecuteTableQuery(string sqlQuery, out string error)
        {
            DataSet set = null;
            try
            {
                set = dbAdapter.ReadData(sqlQuery, out error);
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return set;
        }
        public T ExecuteScalarQuery<T>(string sqlQuery, out string error)
        {
            try
            {
                return dbAdapter.ReadData<T>(sqlQuery, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return default(T);
        }
    }
}
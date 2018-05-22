using System;
using System.Data;
using System.Windows.Input;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.DataModels;
using HotelsApp.Core.RelayCommands;
using HotelsApp.Core.DataModels.Page;
using System.Timers;

namespace HotelsApp.Core.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Fields
        readonly int DefaultMessageDelay = 3000;

        bool pinMessageBox;
        bool showMessageBox;
        MessageType messageType;
        ApplicationPage currentPage;
        TransitionOptions currentTransitionOptions;
        string messageText;
        Timer messageTimer;
        SqlAdapter dbAdapter;
        BaseViewModel currentPageContext;
        #endregion

        #region Public Properties
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
        #endregion

        public ICommand CloseMessageBoxCommand { get; set; }
        public ICommand PinMessageBoxCommand { get; set; }

        public ApplicationViewModel()
        {
            GoTo(ApplicationPage.StartPage, null);

            messageTimer = new Timer
            {
                AutoReset = false
            };
            messageTimer.Elapsed += HideMessageBox;

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
                messageTimer.Interval = DefaultMessageDelay;
                messageTimer.Start();
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

        public DataTable ExecuteTableQuery(string sqlQuery, out string error)
        {
            try
            {
                var table = dbAdapter.ReadData(sqlQuery, out error);
                if (error == null)
                    return table;
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return null;
        }
        public T ExecuteScalarQuery<T>(string sqlQuery, out string error)
        {
            try
            {
                T output = dbAdapter.ReadData<T>(sqlQuery, out error);
                if (error == null)
                    return output;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return default(T);
        }

        void HideMessageBox(object sender, ElapsedEventArgs eventArgs)
        {
            if (!pinMessageBox) ShowMessageBox = false;
            pinMessageBox = false;
        }
    }
}
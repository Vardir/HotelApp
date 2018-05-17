using System;
using System.ComponentModel;

namespace HotelsApp.Core.ViewModels
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
    }
}

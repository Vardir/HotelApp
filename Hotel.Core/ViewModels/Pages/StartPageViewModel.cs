using System.Data;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DBTools;
using HotelsApp.Core.RelayCommands;
using System.Collections.ObjectModel;
using HotelsApp.Core.DataModels.Page;

namespace HotelsApp.Core.ViewModels
{
    public class StartPageViewModel : BasePageViewModel
    {
        public ObservableCollection<HotelViewModel> Hotels { get; }

        public StartPageViewModel()
        {
            Hotels = new ObservableCollection<HotelViewModel>();
        }
        
        protected override void InitializeCommands()
        {
        }

        public void Refresh()
        {
            Hotels.Clear();
            var dataSet = IoCContainer.Application.ExecuteQuery("SELECT * FROM hotel");
            if (dataSet.Tables.Count != 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    Hotels.Add(new HotelViewModel(ItemsFactory.GetHotel(row))
                    {
                        SelectCommand = new RelayParameterizedCommand(SelectHotel)
                    });
                }
            }
        }
        public void SelectHotel(object obj)
        {
            if (obj is HotelViewModel hotel)
            {
                IoCContainer.Application.GoTo(ApplicationPage.HotelPage, hotel);
            }
        }
    }
}
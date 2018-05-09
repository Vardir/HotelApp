using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HotelsApp.Core.IoC;
using HotelsApp.Core.DataModels;
using System.Data;

namespace HotelsApp.Core.ViewModels
{
    public class Category
    {
        public string Title { get; set; }
        public string Tag { get; }
        public ObservableCollection<CategoryItem> CategoryItems { get; set; }

        public Category(string title, string tag)
        {

            Title = title;
            Tag = tag;
        }
    }
    public class CategoryItem
    {
        public string Title { get; set; }
        public string Tag { get; set; }
        public object CommandParameter { get; set; }
        public ICommand Command { get; set; }

        public CategoryItem(string title, string tag, ICommand command)
        {
            Title = title;
            Tag = tag;
            Command = command;
        }
    }
    
    public class StartPageViewModel : BasePageViewModel
    {
        public ObservableCollection<Hotel> Hotels { get; }
        public List<Category> Categories { get; }

        public StartPageViewModel()
        {
            Hotels = new ObservableCollection<Hotel>();
            Categories = new List<Category>();
            InitializeCommands();
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
                    Hotels.Add(new Hotel
                    {
                        Id = (int)row["id"],
                        Title = row["title"] as string,
                        Adress = row["adress"] as string,
                        Image = row["image"] as string,
                        Rating = (double)row["rating"],
                        Reviews = (int)row["reviews"],
                        Stars = (byte)row["stars"],
                        //AvailableRooms = (int)row["available"],
                        //AvgPrices = (double)row["avgprice"]
                    });
                }
            }
        }
    }
}
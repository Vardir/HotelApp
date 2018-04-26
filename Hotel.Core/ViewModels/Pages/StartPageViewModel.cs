using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hotel.Core.ViewModels
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
        public List<Category> Categories { get; }

        public StartPageViewModel()
        {
            Categories = new List<Category>();
            InitializeCommands();            
        }
        
        void InitializeCommands()
        {

        }
    }
}
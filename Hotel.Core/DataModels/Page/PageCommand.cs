using System.Windows.Input;

namespace Hotel.Core.DataModels.Page
{
    public class PageCommand
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public ICommand Command { get; set; }
    }
}
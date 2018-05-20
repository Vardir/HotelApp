using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.ViewModels.Items
{
    public class FacilityViewModel : BaseViewModel
    {
        Facility actualData;

        public int Id => actualData.Id;
        public bool IsSelected
        {
            get => actualData.IsSelected;
            set
            {
                if (actualData.IsSelected != value)
                {
                    actualData.IsSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        public bool WasSelected { get; }
        public string Title
        {
            get => actualData.Title;
            set
            {
                if (actualData.Title != value)
                {
                    actualData.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Tag
        {
            get => actualData.Tag;
            set
            {
                if (actualData.Tag != value)
                {
                    actualData.Tag = value;
                    OnPropertyChanged(nameof(Tag));
                }
            }
        }

        public FacilityViewModel(Facility data = null)
        {
            actualData = data ?? new Facility();
            WasSelected = actualData.IsSelected;
        }
    }
}
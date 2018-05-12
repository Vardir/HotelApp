using System.Windows;
using System.Windows.Controls;

namespace HotelsApp.UI.Controls
{
    /// <summary>
    /// Interaction logic for HotelRating.xaml
    /// </summary>
    public partial class HotelRating : UserControl
    {
        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }
        public int Reviews
        {
            get => (int)GetValue(ReviewsProperty);
            set => SetValue(ReviewsProperty, value);
        }

        public static readonly DependencyProperty ReviewsProperty =
            DependencyProperty.Register(nameof(Reviews), typeof(int), typeof(HotelRating), new PropertyMetadata(0));
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register(nameof(Rating), typeof(double), typeof(HotelRating), new PropertyMetadata(0.0));
        
        public HotelRating()
        {
            InitializeComponent();
        }
    }
}

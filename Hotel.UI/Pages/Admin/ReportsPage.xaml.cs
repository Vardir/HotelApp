using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using HotelsApp.Core.ViewModels;

namespace HotelsApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for StartupPage.xaml
    /// </summary>
    public partial class ReportsPagePage : BasePage<ReportsPageViewModel>
    {
        public ReportsPagePage()
        {
            InitializeComponent();
            Loaded += StartupPage_Loaded;
            PageContextChanged += (context) =>
            {
                if (context is HotelViewModel hotel)
                {
                    ViewModel.Hotel = hotel;
                    ViewModel.Refresh();
                }
            };
            ViewModel.ReportLoaded += ViewModel_ReportLoaded;
            //roomsChart.Series.Add("Rooms").ChartType = SeriesChartType.Line;
            // var x = new string[] { "Owned", "Free" };
            //var y = new int[] { 10, 90 };
            //roomsChart.Series["Rooms"].Points.DataBindXY(x, y);
            //for (int i = 0, j = 0; i < 20; i++, j++)
            //    roomsChart.Series["Rooms"].Points.AddXY(i, j);            
        }

        void StartupPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Refresh();
        }
        void ViewModel_ReportLoaded()
        {
            //string[] x = (from p in ViewModel.DailyRoomsSummary.AsEnumerable()
            //              orderby p.Field<string>("Title") ascending
            //              select p.Field<string>("Title")).ToArray();
            //int[] y = (from p in ViewModel.DailyRoomsSummary.AsEnumerable()
            //           orderby p.Field<string>("Title") ascending
            //           select p.Field<int>("Total")).ToArray();
            //roomsChart.Series["Rooms"].Points.DataBindXY(x ,y);
        }
    }
}
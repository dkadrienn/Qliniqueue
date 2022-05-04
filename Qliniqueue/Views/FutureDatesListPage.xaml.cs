using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureDatesListPage : TabbedPage
    {

        public class Date
        {
            public DateTime dateOfProgramming { get; set; }
            public string DisplayText { get; set; }
            public string DoctorName { get; set; }
        }

        ObservableCollection<Date> futureDates = new ObservableCollection<Date>();
        ObservableCollection<Date> pastDates = new ObservableCollection<Date>();
        List<Date> dates = new List<Date>();
        public ObservableCollection<Date> Dates { get { return futureDates; } }

        public ObservableCollection<Date> Dates2 { get { return futureDates; } }
        public FutureDatesListPage()
        {
            InitializeComponent();

            DateListView.ItemsSource = futureDates;
            DateListView2.ItemsSource = pastDates;

            var x = new DateTime(2022, 5, 7, 15, 30, 0);
            var y = new DateTime(2022, 3, 7, 15, 30, 0);

            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName ="Dr. Alma"});
            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" } );
            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName ="Dr. Alma"} );
            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" } );
            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" });
            dates.Add(new Date { dateOfProgramming = x, DisplayText = x.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" } );
            dates.Add(new Date { dateOfProgramming = y, DisplayText = y.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" });
            dates.Add(new Date { dateOfProgramming = y, DisplayText = y.ToString("yyyy.MM.dd   HH:mm"), DoctorName = "Dr. Alma" });

            foreach (Date i in dates)
            {
                if (DateTime.Compare(i.dateOfProgramming, DateTime.Now) > 0)
                {
                    futureDates.Add(i);
                }
                else
                {
                    pastDates.Add(i);
                }
            }
        }
    }
}
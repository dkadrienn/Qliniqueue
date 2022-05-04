using Newtonsoft.Json.Linq;
using Qliniqueue.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureDatesListPage : TabbedPage
    {
        ObservableCollection<Date> futureDates = new ObservableCollection<Date>();
        ObservableCollection<Date> pastDates = new ObservableCollection<Date>();
        List<Date> dates = new List<Date>();
        public ObservableCollection<Date> Dates { get { return futureDates; } }

        public ObservableCollection<Date> Dates2 { get { return futureDates; } }
        public FutureDatesListPage()
        {
            InitializeComponent();
            string username = Preferences.Get("username", "");

            GetJsonAsync(username);
        }

        public async Task GetJsonAsync(string username)
        {
            string uri_string = "http://192.168.61.131:3000/reservations" + $"?name={username}";
            var uri = new Uri(uri_string);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(content.ToString());
                Debug.WriteLine(jsonArray);

                foreach (var token in jsonArray)
                {
                    Reservation m = new Reservation();
                    m.name = token["name"].ToString();
                    m.age = Convert.ToInt32(token["age"]);
                    m.sex = token["sex"].ToString();
                    m.symptoms = token["symptoms"].ToString();
                    m.diseases = token["diseases"].ToString();
                    m.allergies = token["allergies"].ToString();
                    m.routine = Convert.ToBoolean(token["routine"]);
                    m.doctorId = token["doctorId"].ToString();
                    m.date = Convert.ToDateTime(token["date"]);
                    m.doctorName = token["doctorName"].ToString();

                    Debug.WriteLine(m);
                    dates.Add(new Date { dateOfProgramming = m.date, DisplayText = m.date.ToString("yyyy.MM.dd   HH:mm"), DoctorName = m.doctorName });

                }

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
            DateListView.ItemsSource = futureDates;
            DateListView2.ItemsSource = pastDates;
        }
    }
}
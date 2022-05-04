using Newtonsoft.Json.Linq;
using Qliniqueue.Models;
using System;
using System.Collections.Generic;
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
    public partial class ReservationPage : ContentPage
    {
        private Doctor _doc;
        private string username;
        private Reservation res;
        private bool isReservedTheDate;
        public ReservationPage(Doctor doc)
        {
            InitializeComponent();

            username = Preferences.Get("username", "");
            isReservedTheDate = false;
            _doc = doc;
            lblDoctName.Text = _doc.name;
            lblProfile.Text = _doc.profil;
            lblDescription.Text = _doc.description;
            lblRate.Text = _doc.rate;
            imgDoc.Source = _doc.imageURL;
            
            res = new Reservation();
            res.doctorId = _doc.id;
            res.doctorName = _doc.name;
        }

        public async Task PutJsonAsync()
        {
            var uri = new Uri("http://192.168.61.131:3000/reservations");
            HttpClient httpClient = new HttpClient();

            DateTime date = datepicker.Date;
            var hour = Convert.ToInt32(ntryHour.Text);
            date = date.AddHours(hour);

            res.date = date;
            res.name = username;
            res.age = Convert.ToInt32(ntryAge.Text);
            res.sex = rdbttnMale.IsChecked ? "Male" : "Female";
            res.symptoms = ntrySympt.Text;
            res.diseases = ntryDis.Text;
            res.allergies = ntryAllerg.Text;
            res.routine = rdbttnTrue.IsChecked ? true : false;

            bool isFilled = false;
            if( res.name != null && (res.age < 110 && res.age > 0) && (hour >= 8 && hour < 16) )
            {
                isFilled = true;
                await GetJsonAsync(res.doctorId, date);
            }

            if (isReservedTheDate)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Figyelem", "Ez a dátum már foglalt, vagy egy már lejárt dátumot adott meg!", "OK");
                });
            }
            else if (isFilled)
            {
                var payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, c);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Sikeres foglalás", "Foglalását rögzitettük", "OK");
                });

                FutureDatesListPage futureDatesListPage = new FutureDatesListPage();
                await Navigation.PushAsync(futureDatesListPage);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Figyelem", "A mezők kitöltése érvénytelen!", "OK");
                });
            }
        }

        public async Task GetJsonAsync(string id, DateTime date)
        {
            string uri_string = "http://192.168.61.131:3000/reservations" + $"?doctorId={id}";
            var uri = new Uri(uri_string);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri_string);
            List<DateTime> reservedDates = new List<DateTime>();
            isReservedTheDate = false;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(content.ToString());

                foreach (var token in jsonArray)
                {
                    reservedDates.Add(Convert.ToDateTime(token["date"].ToString()));
                }

                foreach(var d in reservedDates)
                {
                    if(DateTime.Compare(d, date) == 0 || DateTime.Compare(date, DateTime.Now) < 0)
                    {
                        Debug.WriteLine("KAKA");
                        isReservedTheDate = true;
                    }
                }
                
            }
        }

        private void reserv_click(object sender, EventArgs e)
        {
            PutJsonAsync();
        }

    }
}
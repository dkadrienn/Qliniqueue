using Qliniqueue.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationPage : ContentPage
    {
        private Doctor _doc;

        Reservation res;
        public ReservationPage(Doctor doc)
        {
            InitializeComponent();
            _doc = doc;
            lblDoctName.Text = _doc.name;
            lblProfile.Text = _doc.profil;
            lblDescription.Text = _doc.description;
            lblRate.Text = _doc.rate;
            imgDoc.Source = _doc.imageURL;

            res = new Reservation();
            res.doctorId = _doc.id;
        }

        public async Task PutJsonAsync()
        {
            var uri = new Uri("http://192.168.61.131:3000/reservations");
            HttpClient httpClient = new HttpClient();

            DateTime date = datepicker.Date;
            var hour = Convert.ToInt32(ntryHour.Text);
            date = date.AddHours(hour);

            res.date = date;
            res.name = ntryName.Text;
            res.age = Convert.ToInt32(ntryAge.Text);
            res.sex = rdbttnMale.IsChecked ? "Male" : "Female";
            res.symptoms = ntrySympt.Text;
            res.diseases = ntryDis.Text;
            res.allergies = ntryAllerg.Text;
            res.routine = rdbttnTrue.IsChecked ? true : false;

            bool isFilled = false;
            if(res.age != null && res.name != null && (res.age < 110 && res.age > 0) && (hour >= 8 && hour < 16))
            {
                isFilled = true;
            }

            if (isFilled)
            {
                var payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                Debug.WriteLine(payload);


                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, c);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Sikeres foglalás", "Foglalását rögzitettük", "OK");
                });
                await Navigation.PopToRootAsync();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Figyelem", "A mezők kitöltése érvénytelen!", "OK");
                });
            }
        }

        private void reserv_click(object sender, EventArgs e)
        {
            PutJsonAsync();
        }

    }
}
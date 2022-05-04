using Newtonsoft.Json.Linq;
using Qliniqueue.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorDetailPage : ContentPage
    {
        string _id = "1";
        Doctor m;
        public DoctorDetailPage(string id)
        {
            InitializeComponent();
            _id = id;
            m = new Doctor();
            GetJsonAync();
        }


        public async Task GetJsonAync()
        {
            string uri_string = "http://192.168.61.131:3000/doctors" + $"/{_id}";
            Debug.WriteLine(uri_string);
            var uri = new Uri(uri_string);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(content.ToString());
                Debug.WriteLine(jsonObject);

                string id = jsonObject["id"].ToString();
                string name = jsonObject["name"].ToString();
                string imageurl = jsonObject["imageURI"].ToString();
                string profil = jsonObject["profil"].ToString();
                string description = jsonObject["description"].ToString();
                string rate = jsonObject["rate"].ToString();

                m.name = name;
                m.imageURL = imageurl;
                m.profil = profil;
                m.description = description;
                m.rate = rate;
                m.id = id;

                Debug.WriteLine(m);

                img.Source = m.imageURL;
                lblName.Text = m.name;
                lblProfil.Text = m.profil;
                lblDescription.Text = m.description;
                lblRate.Text = m.rate;

            }
        }

        private async void reservation_clicked(object sender, EventArgs e)
        {
            ReservationPage reservationPage = new ReservationPage(m);
            await Navigation.PushAsync(reservationPage);
        }
    }
}
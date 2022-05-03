using Qliniqueue.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Net;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorsPage : ContentPage
    {
        List<Doctor> doctorsList = new List<Doctor>();
        public DoctorsPage()
        {
            InitializeComponent();

            lvDoctors.ItemSelected += lvDoctors_itemClick;

            GetJsonAsync();
        }

        private void lvDoctors_itemClick(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine(doctorsList[e.SelectedItemIndex].name);
        }

        public async Task GetJsonAsync()
        {
            var uri = new Uri("http://192.168.61.131:3000/doctors");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(content.ToString());
                Debug.WriteLine(jsonArray);

                foreach (var token in jsonArray)
                {
                    Doctor m = new Doctor();
                    string id = token["id"].ToString();
                    string name = token["name"].ToString();
                    string imageurl = token["imageURI"].ToString();
                    string profil = token["profil"].ToString();
                    string description = token["description"].ToString();
                    string rate = token["rate"].ToString();

                    m.name = name;
                    m.imageURL = imageurl;
                    m.profil = profil;
                    m.description = description;
                    m.rate = rate;
                    m.id = id;

                    Debug.WriteLine(m);

                    doctorsList.Add(m);
                }
            }
            lvDoctors.ItemsSource = doctorsList;
        }
    }
}
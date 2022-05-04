using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            //bttnDoctors.Text = 
        }

        private async void doctors_Click(object sender, EventArgs e)
        {
            DoctorsPage doctorsPage = new DoctorsPage();
            await Navigation.PushAsync(doctorsPage);
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Qliniqueue.Tables;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void bttnRegister_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UsersDB.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<RegisterUsersTable>();

            var user = new RegisterUsersTable()
            {
                Username = entryUsername.Text,
                Email = entryEmail.Text,
                Password = entryPassword.Text
            };
            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
            {
                db.Insert(user);
                Console.WriteLine(user);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Információ", "Sikeres regisztráció!", "OK", ":)");
                    if (result) await Navigation.PushAsync(new LoginPage());
                });
            }
            else
                DisplayAlert("Hiba", "Nincs kitöltve minden mező!", "OK", "_");

        }
    }
}
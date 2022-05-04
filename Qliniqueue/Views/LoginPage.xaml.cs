using Qliniqueue.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qliniqueue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void bttnLogin_Clicked(object sender, EventArgs e)//
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UsersDB.db");
            var db = new SQLiteConnection(dbPath);
            var query = db.Table<RegisterUsersTable>()
                            .Where(u => u.Username.Equals(entryUsername.Text) ||
                                    u.Email.Equals(entryUsername.Text) &&
                                    u.Password.Equals(entryPassword.Text))
                            .FirstOrDefault();

            Preferences.Set("username", entryUsername.Text);

            if (query != null)
            {
                App.Current.MainPage = new AppShell();           
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Hiba", "Sikertelen bejelentkezés!", "OK", ":(");
                    if (result) await Navigation.PushAsync(new LoginPage());
                });
            }
        }

        async void bttnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}

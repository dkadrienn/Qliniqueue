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
            db.Insert(user);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Welcome!", "Successfull registration!", "Yes", "Cancel");
            });
        }
    }
}
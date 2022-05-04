using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void LoginTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Bejelentkezés"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void HintTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Nincs még regisztrálva?"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void NameEntrDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Felhasználónév/Email"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void PasswordEntrDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Jelszó"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void RegistrationDisplayed()
        {
            app.Tap(c => c.Marked("Regisztráció"));
            app.Screenshot("Login push button");

            AppResult[] results = app.WaitForElement(c => c.Marked("Regisztráció"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void RegistrationNameEntryDisplayed()
        {
            app.Tap(c => c.Marked("Regisztráció"));
            app.Screenshot("Login push button");

            AppResult[] results = app.WaitForElement(c => c.Marked("Felhasználónév"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }
        
        [Test]
        public void RegistrationEmailEntryDisplayed()
        {
            app.Tap(c => c.Marked("Regisztráció"));
            app.Screenshot("Login push button");

            AppResult[] results = app.WaitForElement(c => c.Marked("Email"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }
        
        [Test]
        public void RegistrationPasswordEntryDisplayed()
        {
            app.Tap(c => c.Marked("Regisztráció"));
            app.Screenshot("Login push button");

            AppResult[] results = app.WaitForElement(c => c.Marked("Jelszó"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }

        //[Test]
        //public void Login()
        //{
        //    app.EnterText(c => c.Marked("Felhasználónév/Email"), "zsolt");
        //    app.EnterText(c => c.Marked("Jelszó"), "123456789");

        //    app.Screenshot("Login data entry");

        //    app.Tap(c => c.Marked("Bejelentkezés"));
        //    app.Screenshot("Login push button");

        //    AppResult[] result = app.Query(c => c.Id("Hiba").Text("Sikertelen bejelentkezés!"));
        //    Assert.IsTrue(result.Any(), "The error message is being displayed");
        //}
    }
}

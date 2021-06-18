using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E1201710050004
{
    public partial class App : Application
    {
        public static string UBICACIONDB1 = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(String dbexamenpm2)
        {
            InitializeComponent();
            UBICACIONDB1 = dbexamenpm2;
            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

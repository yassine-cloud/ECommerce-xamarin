using System;
using ECommerce.auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new ProduitPage());
            //MainPage = new NavigationPage(new AcceuilPage());
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            /*var token = await SessionManager.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                // User is logged in
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                // Redirect to login page
                await Shell.Current.GoToAsync("//LoginPage");
            }*/
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

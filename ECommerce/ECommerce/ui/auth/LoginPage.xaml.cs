using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.auth;
using ECommerce.services;
using ECommerce.ui.auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private authService authService;
        public LoginPage()
        {
            InitializeComponent();
            authService = new authService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            // Simulate login (replace with real authentication logic)
            bool isLoginSuccessful = AuthenticateUserAsync(email, password);

            if (isLoginSuccessful)
            {
                await DisplayAlert("Success", "Welcome back!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        // Mock authentication function (replace with backend API call)
        private bool AuthenticateUserAsync(string email, string password)
        {
            //return Task.FromResult(email == "user" && password == "password"); // Replace with real logic
            return authService.Login(email, password);

        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            // Navigate to the registration page
            await Navigation.PushAsync(new RegisterPage());
        }

        protected override bool OnBackButtonPressed()
        {
            
             Shell.Current.GoToAsync("//AcceuilPage");
             return true; // Prevent exiting the app
            
        }
    }
}
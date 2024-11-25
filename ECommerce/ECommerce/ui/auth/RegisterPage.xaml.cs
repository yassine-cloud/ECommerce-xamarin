using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        private authService authService;
        public RegisterPage ()
		{
			InitializeComponent ();
            authService = new authService();
        }
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(NomEntry.Text) ||
                string.IsNullOrWhiteSpace(PrenomEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(CPasswordEntry.Text)||
                string.IsNullOrWhiteSpace(ContactEntry.Text)) 
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }
            if (!CPasswordEntry.Text.Equals(PasswordEntry.Text)) { 
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }
                
            if (PasswordEntry.Text.Length < 6)
               { 
                await DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
                return;
            }
            if (ContactEntry.Text.Length != 8){
                await DisplayAlert("Error", "Contact must be 8 characters long.", "OK");
                return;
            }
            // Create a new user object
            var newUser = new UserCreate
            {
                nom = NomEntry.Text,
                prenom = PrenomEntry.Text,
                email = EmailEntry.Text,
                password = PasswordEntry.Text,
                role = Role.USER,
                contact = ContactEntry.Text
            };

            // TODO: Replace with actual backend API call
            bool isRegistered = RegisterUserAsync(newUser);
            if (isRegistered)
            {
                await DisplayAlert("Success", "Registration completed!", "OK");
                // Navigate to login page or home
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Registration failed. Try again.", "OK");
            }
        }

        private bool RegisterUserAsync(UserCreate user)
        {
            // Simulate API call

            //string userJson = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            //await DisplayAlert("deb", "User :" + userJson, "OK");

            //return false;
            return authService.Register(user);
        }
    }
}
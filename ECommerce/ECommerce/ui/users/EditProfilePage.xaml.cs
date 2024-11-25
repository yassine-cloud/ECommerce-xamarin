using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ECommerce.auth;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.users
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(SessionManager.GetUser(), Navigation);
        }
    }

    public class EditProfileViewModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Password { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly INavigation _navigation;
        private readonly UserRead _originalUser;

        private UserService userService;

        public EditProfileViewModel(UserRead user, INavigation navigation)
        {
            _originalUser = user;
            _navigation = navigation;

            // Initialize fields
            Nom = user.nom;
            Prenom = user.prenom;
            Email = user.email;
            Contact = user.contact;
            

            userService = new UserService();

            // Commands
            SaveCommand = new Command(async () => await SaveProfile());
            CancelCommand = new Command(async () => await CancelEdit());
        }

        private async Task SaveProfile()
        {
            var finalPassword = string.IsNullOrWhiteSpace(Password) ? _originalUser.password : Password;
            // Create an updated user object
            var updatedUser = new UserUpdate
            {
                id = _originalUser.id,
                nom = Nom,
                prenom = Prenom,
                email = Email,
                contact = Contact,
                password = finalPassword,
                role = _originalUser.role // Keep the original role
            };


            // Call API to update user
            var success = userService.updateUser(updatedUser); // Replace with your actual API client

            if (success)
            {
                // Update session information
                var newUser = new UserRead
                {
                    id = _originalUser.id,
                    nom = updatedUser.nom,
                    prenom = updatedUser.prenom,
                    email = updatedUser.email,
                    contact = updatedUser.contact,
                    password = updatedUser.password,
                    role = updatedUser.role,
                    paniers = _originalUser.paniers // Keep the original paniers
                };

                SessionManager.SaveSession(await SessionManager.GetTokenAsync(), newUser);
                await Application.Current.MainPage.DisplayAlert("Success", "Profile updated successfully!", "OK");
                await _navigation.PopAsync(); // Return to previous page
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update profile. Try again.", "OK");
            }
        }

        private async Task CancelEdit()
        {
            await _navigation.PopAsync(); // Return to the previous page
        }
    }
    }
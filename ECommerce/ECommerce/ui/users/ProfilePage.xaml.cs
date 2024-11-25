using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.auth;
using ECommerce.models;
using ECommerce.ui.users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
            var user = SessionManager.GetUser();
            if (user == null)
            {
                Shell.Current.GoToAsync("//AcceuilPage/LoginPage");
                DisplayAlert("Session Expired", "Please log in to access your profile.", "OK");
                //Navigation.PushAsync(new LoginPage());
               
                return;
            }
            InitializeComponent ();
            BindingContext = new ProfileViewModel(SessionManager.GetUser(), Navigation);
            // Check if user is logged in

            btnShowUsers.IsVisible = SessionManager.GetUser().role == Role.ADMIN;
            BindingContext = new ProfileViewModel(SessionManager.GetUser(), Navigation);
        }

    }

    public class ProfileViewModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Role { get; set; }

        public Command GoToPanierCommand { get; }
        public Command EditProfileCommand { get; }
        public Command LogoutCommand { get; }

        public ProfileViewModel(UserRead user, INavigation navigtation)
        {
            Nom = user.nom;
            Prenom = user.prenom;
            Email = user.email;
            Contact = user.contact;
            Role = user.role.ToString();
            _navigation = navigtation;

            GoToPanierCommand = new Command(OnGoToPanier);
            EditProfileCommand = new Command(OnEditProfile);
            LogoutCommand = new Command(OnLogout);
        }

        private readonly INavigation _navigation;

        public ProfileViewModel(INavigation navigation, UserRead user)
        {
            _navigation = navigation;
            Nom = user.nom;
            Prenom = user.prenom;
            Email = user.email;
            Contact = user.contact;
            Role = user.role.ToString();

            GoToPanierCommand = new Command(OnGoToPanier);
            EditProfileCommand = new Command(OnEditProfile);
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnGoToPanier()
        {
            await _navigation.PushAsync(new CartPage());
        }

        private async void OnEditProfile()
        {
            await _navigation.PushAsync(new EditProfilePage());
        }

        private void OnLogout()
        {
            SessionManager.ClearSession();
            Application.Current.MainPage = new AppShell(); // Restart to reset session
        }
    }
}
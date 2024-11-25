using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcceuilPage : ContentPage
	{
        public AcceuilPage()
        {
            InitializeComponent();
        }

        // Event handler for the "Voir les Produits" button
        private async void OnViewProductsClicked(object sender, EventArgs e)
        {
            // Navigate to the Products page
            //await Navigation.PushAsync(new ProduitPage());
            await Shell.Current.GoToAsync("//ProduitPage");
        }

        // Event handler for the "Mon Profil" button
        private async void OnProfileClicked(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserConnected())
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }
            // Navigate to the Profile page (ensure you have a ProfilePage defined)
            await Shell.Current.GoToAsync("//ProfilePage");
            //await Navigation.PushAsync(new ProfilePage());
        }

        // Event handler for the "Mon Panier" button
        private async void OnCartClicked(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserConnected())
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }
            // Navigate to the Cart page (ensure you have a CartPage defined)
            await Navigation.PushAsync(new CartPage());
        }

        // Event handler for the "Se Déconnecter" button
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Logic for logout (e.g., clear session, redirect to login)
            bool confirmLogout = await DisplayAlert("Déconnexion", "Êtes-vous sûr de vouloir vous déconnecter ?", "Oui", "Non");
            if (confirmLogout)
            {
                // Example: Redirect to a login page
                await Navigation.PushAsync(new LoginPage());
            }
        }

    }
}
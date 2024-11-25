using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.users
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsersPage : ContentPage
	{
        private UserService userService;
        private List<UserRead> userReads;
        private List<UserRead> Users;

        public UsersPage()
		{
            
                InitializeComponent();
            // Bind the Users list to the CollectionView
            userService = new UserService();
            //LoadUsers();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Call LoadUsers to refresh data whenever the page appears
            LoadUsers();
        }



        private async void LoadUsers()
        {
            // Show a loading indicator (optional)
            IsBusy = true;

            try
            {
                // Run the data loading and transformation in the background thread
                userReads = await Task.Run(() => userService.getAllUsers());

                // If no users found, initialize it as an empty list
                if (userReads == null)
                    userReads = new List<UserRead>();

                // Ensure that the data transformation works correctly
                Users = userReads;
                // Set the ItemsSource of the CollectionView to the Users list
                UsersListView.ItemsSource = Users;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., show an error message)
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                // Hide the loading indicator
                IsBusy = false;
            }
        }
            private async void OnUserSelected(object sender, SelectionChangedEventArgs e)
            {
                if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
                {
                    var selectedUser = e.CurrentSelection[0] as UserRead;
                    // Handle the selected user (e.g., navigate to details page or display a dialog)
                    await DisplayAlert("User Selected", $"Name: {selectedUser.nom}\nID: {selectedUser.id}", "OK");

                    // Clear selection to allow reselection
                    ((CollectionView)sender).SelectedItem = null;
                }
            }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // Navigate to product details page (optional)
                UserRead selectedUser = e.SelectedItem as UserRead;
                if (selectedUser == null)
                    return;
                Navigation.PushAsync(new CartPage(selectedUser.id));
            }
        }
    }


}
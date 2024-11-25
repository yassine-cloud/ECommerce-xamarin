using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ECommerce.auth;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private PanierService panierService; // Service to get panier data
        private List<IntegratedPanier> Paniers; // List of integrated panier data
        private List<PanierRead> lpaniers; // List to hold the original panier data
        private string userId;

        public ICommand BackToProfileCommand { get; }

        public CartPage(string userId)
        {
            InitializeComponent();
            if (userId != null ) 
                this.userId = userId;
            else
                this.userId = SessionManager.GetUser().id;

            // Initialize the panierService
            panierService = new PanierService();

            // Initialize the BackToProfileCommand
            BackToProfileCommand = new Command(OnBackToProfile);

            // Call the LoadPanierData method asynchronously to avoid blocking the UI thread
            LoadPanierData();
        }

        // Async method to load the panier data
        private async void LoadPanierData()
        {
            // Show a loading indicator (optional)
            IsBusy = true;

            try
            {
                // Run the data loading and transformation in the background thread
                lpaniers = await Task.Run(() => panierService.GetUserPanier(userId));

                // If no paniers found, initialize it as an empty list
                if (lpaniers == null)
                    lpaniers = new List<PanierRead>();

                // Ensure that the data transformation works correctly
                Paniers = lpaniers.Select(p => new IntegratedPanier(p)).ToList();

                // Update the ListView with the transformed data
                PaniersListView.ItemsSource = Paniers;

                // Calculate the total price by summing up the prices of all items
                UpdateTotalPrice();
            }
            catch (Exception ex)
            {
                // Log the error message if any
                Console.WriteLine("Error loading panier data: " + ex.Message);
            }
            finally
            {
                // Hide the loading indicator (optional)
                IsBusy = false;
            }
        }

        private async void OnBackToProfile()
        {
            await Navigation.PopAsync(); // Navigate back to the profile page
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is IntegratedPanier panier)
            {
                DeleteItem(panier);
            }
        }


        // Method to delete an item
        private async void DeleteItem(IntegratedPanier panier)
        {
            bool response =  await DisplayAlert("Delete", $"Are you sure you want to delete {panier.ProductDesignation}?", "Yes", "No");

            if(response) {
                

                bool res = panierService.DeletePanier(panier.PanierId);
                if (!res)
                {
                    await DisplayAlert("Error", "Failed to delete product from cart.", "OK");
                }
                else
                {
                    // Remove the item from the list
                    Paniers.Remove(panier);
                    // Refresh the ListView
                    PaniersListView.ItemsSource = null;
                    PaniersListView.ItemsSource = Paniers;

                    // Update the total price
                    UpdateTotalPrice();
                }
            }

            //// Remove the item from the list
            //Paniers.Remove(panier);

            //// Refresh the ListView
            //PaniersListView.ItemsSource = null;
            //PaniersListView.ItemsSource = Paniers;

            //// Update the total price
            //UpdateTotalPrice();
        }

        // Method to calculate and update the total price
        private void UpdateTotalPrice()
        {
            
            if (Paniers.Count == 0)
            {
                TotalPriceLabel.Text = "Grand Total: $0.00"; // Reset the total price label if there are no items in the cart
                DeleteButton.IsVisible = false;
            }
            else
            {
                double totalPrice = Paniers.Sum(p => p.TotalPrice);
                TotalPriceLabel.Text = $"Grand Total: {totalPrice:C}";
            }
        }

        private async void OnDeleteAllButtonClicked(object sender, EventArgs e)
        {
            // Confirm action with the user
            bool response = await DisplayAlert("Delete All", "Are you sure you want to delete all items from your cart?", "Yes", "No");

            if (response)
            {
                try
                {
                    // Loop through each item in the Paniers list and call the DeleteItem method
                    foreach (var panier in Paniers.ToList()) // ToList() is used to avoid modifying the collection while iterating
                    {
                        // Call DeleteItem method to delete each panier
                        DeleteItemWithPermission(panier);
                    }

                    // After deleting all items, update the UI
                    Paniers.Clear();
                    PaniersListView.ItemsSource = null;
                    PaniersListView.ItemsSource = Paniers;

                    // Update the total price to 0
                    UpdateTotalPrice();

                    // Inform the user
                    await DisplayAlert("Success", "All items have been deleted from your cart.", "OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting all items: " + ex.Message);
                    await DisplayAlert("Error", "An error occurred while deleting items.", "OK");
                }
            }
        }

        private  void DeleteItemWithPermission(IntegratedPanier panier)
        {

            panierService.DeletePanier(panier.PanierId);

        }
            
    }

    public class IntegratedPanier
    {
        public string PanierId { get; set; }
        public string ProductDesignation { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPhoto { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => ProductPrice * Quantity;  // Calculate total price per item

        // Constructor to easily populate the integrated class
        public IntegratedPanier(PanierRead panier)
        {
            PanierId = panier.id;
            ProductDesignation = panier.produit.designation;
            ProductPrice = panier.produit.prix;
            ProductPhoto = panier.produit.photo;
            Quantity = panier.quantite;
        }
    }
}
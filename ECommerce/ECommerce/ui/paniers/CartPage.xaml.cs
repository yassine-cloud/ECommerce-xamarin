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

        public ICommand BackToProfileCommand { get; }

        public CartPage()
        {
            InitializeComponent();

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
                lpaniers = await Task.Run(() => panierService.GetUserPanier(SessionManager.GetUser().id));

                // If no paniers found, initialize it as an empty list
                if (lpaniers == null)
                    lpaniers = new List<PanierRead>();

                // Ensure that the data transformation works correctly
                Paniers = lpaniers.Select(p => new IntegratedPanier(p)).ToList();

                // Update the ListView with the transformed data
                PaniersListView.ItemsSource = Paniers;

                // Calculate the total price by summing up the prices of all items
                double totalPrice = lpaniers.Sum(p => p.produit.prix * p.quantite);
                TotalPriceLabel.Text = $"Grand Total: {totalPrice:C}";
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
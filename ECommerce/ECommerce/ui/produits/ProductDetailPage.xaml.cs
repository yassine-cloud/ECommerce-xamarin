using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.auth;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public Produit Product { get; set; }
        public string CategoryName { get; set; }

        private PanierService PanierService;

        // Constructor to accept both the Product and Category Name
        public ProductDetailPage(Produit product, string categoryName)
        {
            InitializeComponent();

            // Set the properties
            Product = product;
            CategoryName = "Categorie : " + categoryName;

            PanierService = new PanierService();

            // Bind the data to the page
            BindingContext = this;
        }

        // Add to Cart button logic
        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (!SessionManager.IsUserConnected())
            {
                await DisplayAlert("Not Logged In", "Please log in to add products to your cart.", "OK");
                await Navigation.PushAsync(new LoginPage());
                return;
            }

            // Show a popup to select quantity
            string result = await DisplayPromptAsync(
                "Select Quantity",
                "Enter the number of products:",
                "OK",
                "Cancel",
                "1",
                keyboard: Keyboard.Numeric);

            if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int quantity) || quantity <= 0)
            {
                await DisplayAlert("Invalid Quantity", "Please enter a valid quantity.", "OK");
                return;
            }

            // Add the product to the cart
            AddToCart(Product, quantity);

            // Confirm addition to the cart
            //await DisplayAlert("Cart", "Product added to cart!", "OK");
        }

        private async void AddToCart(Produit product, int quantity)
        {
            // Logic to add the product to the cart
            // Example:
            // CartManager.AddItem(new CartItem { Product = product, Quantity = quantity });
            PanierCreate panierCreate = new PanierCreate
            {
                produitId = product.id,
                userId = SessionManager.GetUser().id,
                quantite = quantity
            };

            bool created = PanierService.AddPanier(panierCreate);
            if (!created)
            {
                await DisplayAlert("Error", "Failed to add product to cart.", "OK");
            }
            else
            {
                // Update the cart icon (optional)
                // MessagingCenter.Send(this, "UpdateCartIcon");
                await DisplayAlert("Cart", "Product added to cart!", "OK");
            }
        }
    }
}
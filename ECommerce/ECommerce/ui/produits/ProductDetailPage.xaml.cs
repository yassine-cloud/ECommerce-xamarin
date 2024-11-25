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

        private Produit produitIn;

        // Constructor to accept both the Product and Category Name
        public ProductDetailPage(Produit product, string categoryName)
        {
            InitializeComponent();

            // Set the properties
            produitIn = product;
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

            PanierUpdate panierUpdate = getPanierIfAvailable();
            if (panierUpdate != null)
            {
                updateCart(panierUpdate);
            }
            else
            {
                createCart();
            }
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

        private async void createCart()
        {
            // Show a popup to select quantity
            string result = await DisplayPromptAsync(
                "Select Quantity",
                "Enter the number of products:",
                "OK",
                "Cancel",
                "1",
                keyboard: Keyboard.Numeric);

            if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int quantity) || quantity < 0)
            {
                await DisplayAlert("Invalid Quantity", "Please enter a valid quantity.", "OK");
                return;
            }
            if (quantity == 1 && produitIn.prix > 2000)
            {

                bool answer = await DisplayAlert("Alert", "Are you sure you want to buy more than one product with a price greater than 2000?", "Yes", "No");
                if (answer)
                {
                    // Add the product to the cart
                    AddToCart(Product, quantity);
                    return;
                }
                else return;
            }
            if (quantity > 1 && produitIn.prix > 2000)
            {
                await DisplayAlert("Alert", "You can't buy more than one product with a price greater than 2000.", "OK");
                return;
            }
            if (quantity > 1 && produitIn.prix <= 2000)
            {
                // Add the product to the cart
                AddToCart(Product, quantity);
                return;
            }
            if (quantity == 1 && produitIn.prix <= 2000)
            {
                // Add the product to the cart
                AddToCart(Product, quantity);
                return;
            }
            if (produitIn.prix > 2000)
            {
                await DisplayAlert("Alert", "You can't buy this product because its price is greater than 2000.", "OK");
                return;
            }
        }
        
        private async void updateCart(PanierUpdate panierUpdate)
        {
            // show a popup to select quantity incement it or decrement it
            string result = await DisplayPromptAsync(
                "Select Quantity",
                "You have already the product in your cart select new quantity:",
                "OK",
                "Cancel",
                "1",
                keyboard: Keyboard.Numeric,
                initialValue: panierUpdate.quantite.ToString());

            if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int quantity) || quantity<0 ) {
                await DisplayAlert("Invalid Quantity", "Please enter a valid quantity.", "OK");
                return;
            }
            if (quantity == 1 && produitIn.prix > 2000)
            {

                bool answer = await DisplayAlert("Alert", "Are you sure you want to buy more than one product with a price greater than 2000?", "Yes", "No");
                if (answer)
                {
                    // Add the product to the cart
                    panierUpdate.quantite = quantity;
                    bool updated = PanierService.UpdatePanier(panierUpdate);
                    if (!updated)
                    {
                        await DisplayAlert("Error", "Failed to update product in cart.", "OK");
                    }
                    else
                    {
                        // Update the cart icon (optional)
                        // MessagingCenter.Send(this, "UpdateCartIcon");
                        await DisplayAlert("Cart", "Product updated in cart!", "OK");
                    }
                    return;
                }
                else return;
            }

            if (quantity > 1 && produitIn.prix > 2000)
            {
                await DisplayAlert("Alert", "You can't buy more than one product with a price greater than 2000.", "OK");
                return;
            }

            if (quantity > 1 && produitIn.prix <= 2000)
            {
                // Add the product to the cart
                panierUpdate.quantite = quantity;
                bool updated = PanierService.UpdatePanier(panierUpdate);
                if (!updated)
                {
                    await DisplayAlert("Error", "Failed to update product in cart.", "OK");
                }
                else
                {
                    // Update the cart icon (optional)
                    // MessagingCenter.Send(this, "UpdateCartIcon");
                    await DisplayAlert("Cart", "Product updated in cart!", "OK");
                }
                return;
            }

            if (quantity == 1 && produitIn.prix <= 2000)
            {
                // Add the product to the cart
                panierUpdate.quantite = quantity;
                bool updated = PanierService.UpdatePanier(panierUpdate);
                if (!updated)
                {
                    await DisplayAlert("Error", "Failed to update product in cart.", "OK");
                }
                else
                {
                    // Update the cart icon (optional)
                    // MessagingCenter.Send(this, "UpdateCartIcon");
                    await DisplayAlert("Cart", "Product updated in cart!", "OK");
                }
                return;
            }

            if (produitIn.prix > 2000)
            {
                await DisplayAlert("Alert", "You can't buy this product because its price is greater than 2000.", "OK");
                return;
            }

        }

        private PanierUpdate getPanierIfAvailable()
        {
            string userId = SessionManager.GetUser().id;
            List<PanierRead> paniers = PanierService.GetUserPanier(userId);
            PanierRead panier = paniers.FirstOrDefault(p => p.produit.id == produitIn.id);
            if (panier != null)
            {
                return new PanierUpdate
                {
                    id = panier.id,
                    quantite = panier.quantite,
                    produitId = panier.produit.id,
                    userId = panier.user
                };
            }
            return null;

        }
    }
}
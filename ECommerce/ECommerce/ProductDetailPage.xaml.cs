using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public Produit Product { get; set; }
        public string CategoryName { get; set; }

        // Constructor to accept both the Product and Category Name
        public ProductDetailPage(Produit product, string categoryName)
        {
            InitializeComponent();

            // Set the properties
            Product = product;
            CategoryName = "Categorie : " + categoryName;

            // Bind the data to the page
            BindingContext = this;
        }

        // Add to Cart button logic
        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            // Logic to add the product to the cart
            DisplayAlert("Cart", "Product added to cart!", "OK");
        }
    }
}
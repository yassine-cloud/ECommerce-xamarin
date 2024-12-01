using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.produits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPage : ContentPage
    {
        private readonly ProduitService _produitService;
        private readonly Produit _product;

        public EditProductPage(Produit product)
        {
            InitializeComponent();
            _produitService = new ProduitService();
            _product = product;

            // Pre-fill the fields with the product's current data
            designationEntry.Text = _product.designation;
            prixEntry.Text = _product.prix.ToString();
            photoEntry.Text = _product.photo;
        }

        // Update Product Button Handler
        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {
            // Validate user input
            string designation = designationEntry.Text;
            string photo = photoEntry.Text;
            string prixText = prixEntry.Text;

            if (string.IsNullOrWhiteSpace(designation) ||
                string.IsNullOrWhiteSpace(photo) ||
                string.IsNullOrWhiteSpace(prixText) ||
                !double.TryParse(prixText, out double prix))
            {
                await DisplayAlert("Error", "Please fill all fields correctly.", "OK");
                return;
            }

            if (prix < 0)
            {
                await DisplayAlert("Error", "Price cannot be negative.", "OK");
                return;
            }

            // Update the product
            var updatedProduct = new ProduitUpdate
            {
                id = _product.id,
                designation = designation,
                prix = prix,
                photo = photo,
            };

            try
            {
                // Call the update service
                bool success = _produitService.UpdateProduit( updatedProduct);
                if (success)
                {
                    await DisplayAlert("Success", "Product updated successfully.", "OK");
                    await Navigation.PopAsync(); // Return to the previous page
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update product.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
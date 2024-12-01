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
    public partial class AddProductPage : ContentPage
    {
        private readonly ProduitService _produitService;
        private readonly string _categoryName;
        private CategorieRead category;

        public AddProductPage(CategorieRead category)
        {
            InitializeComponent();
            _produitService = new ProduitService();
            this.category = category;
            _categoryName = category.nom;
        }

        // Add Product Button Handler
        private async void OnAddProductClicked(object sender, EventArgs e)
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

            if(prix < 0)
            {
                await DisplayAlert("Error", "Price cannot be negative.", "OK");
                return;
            }

            // Create the product
            var newProduct = new ProduitCreate
            {
                designation = designation,
                prix = prix,
                photo = photo,
            };

            try
            {
                // Save product using the service
                bool success = _produitService.AddProduit(newProduct, category.id);
                if (success)
                {
                    await DisplayAlert("Success", "Product added successfully.", "OK");
                    await Navigation.PopAsync(); // Return to the previous page
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add product.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
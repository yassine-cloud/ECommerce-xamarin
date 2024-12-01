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
    public partial class AdminProduct : ContentPage
    {
        private readonly ProduitService _produitService;
        private readonly string _categoryName;
        public List<Produit> Produits { get; set; }
        private CategorieRead category;
        public string CategoryTitle { get; set; }

        public AdminProduct(CategorieRead category)
        {
            InitializeComponent();

            _produitService = new ProduitService();
            this.category = category;
            _categoryName = category.nom;
            CategoryTitle = $"Products in: {category.nom}";
            BindingContext = this;

            LoadProduits();
        }

        // Load products from service
        private void LoadProduits()
        {
            try
            {
                Produits = _produitService.GetProduits(category.id);
                produitListView.ItemsSource = Produits;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProduits();
        }

        // Add Product Button
        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductPage(category));
        }

        // Edit Product Button
        private async void OnEditProductClicked(object sender, EventArgs e)
        {
            var product = (Produit)((ImageButton)sender).CommandParameter;
            await Navigation.PushAsync(new EditProductPage(product));
        }

        // Delete Product Button
        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            var product = (Produit)((ImageButton)sender).CommandParameter;
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this product?", "Yes", "No");
            if (confirm)
            {
                bool success = _produitService.DeleteProduit(product.id);
                if (success)
                {
                    await DisplayAlert("Success", "Product deleted successfully.", "OK");
                    LoadProduits();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete product.", "OK");
                }
            }
        }

        // Handle Item Selection (optional)
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; // Deselect item
        }
    }
}
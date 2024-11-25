using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProduitPage : ContentPage
    {
        private ProduitService produitService;
        private CategorieService categorieService;

        private List<Produit> produits;
        private List<Produit> produitsFiltered;
        private List<CategorieRead> categories;
        private string selectedCategoryId;

        public ProduitPage()
        {
            InitializeComponent();
            produitService = new ProduitService();
            categorieService = new CategorieService();

            // Set the categories list
            produits = produitService.GetProduits();
            produitsFiltered = produits;
            categories = categorieService.GetCategories();
            selectedCategoryId = null;

            // Add "ALL" option to categories
            var allCategory = new CategorieRead { id = null, nom = "ALL" };
            categories.Insert(0, allCategory);

            categoryPicker.ItemsSource = categories.Select(c => c.nom).ToList();

            produitListView.ItemsSource = produitsFiltered;
        }


        private void LoadProducts()
        {
            // Get the selected category (if any)
            string selectedCategory = categoryPicker.SelectedItem?.ToString();

            // Filter the products by category
            // get the id of the selected category
            if (selectedCategory != null && selectedCategory != "ALL")
            {
                var selectedCategoryObject = categories.FirstOrDefault(c => c.nom == selectedCategory);
                selectedCategoryId = selectedCategoryObject.id;
                produitsFiltered = produits.Where(p => p.categorie == selectedCategoryId).ToList();
            }
            else
            {
                produitsFiltered = produits;
            }

            // Bind the ListView to the products list
            produitListView.ItemsSource = produitsFiltered;
        }

        // Handle the category change (filter the products)
        private void OnCategoryChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        // Handle item selection (optional)
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // Navigate to product details page (optional)
                var produit = e.SelectedItem as Produit;
                if (produit != null)
                {
                    // For example, navigate to a new page showing the product details
                    string categoriename = categories.FirstOrDefault(c => c.id == produit.categorie)?.nom;
                    Navigation.PushAsync(new ProductDetailPage(produit, categoriename));
                }
            }
        }
    }
}
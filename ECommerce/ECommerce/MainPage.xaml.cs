using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;

namespace ECommerce
{
    public partial class MainPage : ContentPage
    {
        private ProduitService _produitService = new ProduitService();
        public ObservableCollection<Produit> Products { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Produit>();
            ProductList.ItemsSource = Products;
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    await LoadProductsAsync();
        //}

        //private async Task<object> LoadProductsAsync(string category = null)
        //{

        //    //var productsJson = await _produitService.GetProduitsByCategoryAsync(category);
        //    // Deserialize productsJson to your Product model and populate the ObservableCollection
        //    return null;
        //}
        
        //private async void OnCategorySelected(object sender, EventArgs e)
        //{
        //    var selectedCategory = CategoryPicker.SelectedItem as string;
        //    await LoadProductsAsync(selectedCategory);
        //}
    }
}

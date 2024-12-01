using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using ECommerce.ui.produits;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriePage : ContentPage
    {
        private readonly CategorieService _categorieService;
        public List<CategorieRead> Categories { get; set; }
        public CategorieRead SelectedCategory { get; set; }

        public CategoriePage()
        {
            InitializeComponent();
            _categorieService = new CategorieService();
            Categories = new List<CategorieRead>();
            BindingContext = this;

            LoadCategories();
        }

        // Load categories from the service
        private async void LoadCategories()
        {
            try
            {
                // Load categories using the service
                Categories = _categorieService.GetCategories();
                OnPropertyChanged(nameof(Categories));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCategories();
        }

        // Add category button click event
        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            // Navigate to the Add Category Page
            await Navigation.PushAsync(new AddCategorie());
        }

        // Edit category command
        private async void OnEditCategory(CategorieRead category)
        {
            // Navigate to the Edit Category Page (you can create an edit page if needed)
            await Navigation.PushAsync(new EditCategoriePage(category));
        }

        private async void OnEditCategoryClicked(object sender, EventArgs e)
        {
            // Get the category object associated with this delete button
            var category = ((ImageButton)sender).CommandParameter as CategorieRead;

            if (category != null)
            {
                await Navigation.PushAsync(new EditCategoriePage(category));
            }
        }

        // Delete category command
        private async void OnDeleteCategoryClicked(object sender, EventArgs e)
        {
            // Get the category object associated with this delete button
            var category = ((ImageButton)sender).CommandParameter as CategorieRead;

            if (category != null)
            {
                bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this category?", "Yes", "No");
                if (confirm)
                {
                    bool result = _categorieService.DeleteCategorie(category.id);
                    if (result)
                    {
                        LoadCategories();
                        await DisplayAlert("Success", "Category deleted successfully.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete category. Check dependencies.", "OK");
                    }
                }
            }
        }

        // Handle selection of a category
        private async void OnCategorySelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedCategory = e.SelectedItem as CategorieRead;
                if (selectedCategory != null)
                {
                    // await DisplayAlert("Category Selected", $"Category: {selectedCategory.nom}", "OK");
                    // You can perform actions with the selected category, like navigating to a details page.
                    //await Navigation.PushAsync(new EditCategoriePage(selectedCategory));
                    await Navigation.PushAsync(new AdminProduct(selectedCategory));
                }

                // Clear selection
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using ECommerce.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerce.ui.categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCategorie : ContentPage
    {
        private readonly CategorieService _categorieService;

        public AddCategorie()
        {
            InitializeComponent();
            _categorieService = new CategorieService();
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            // Get data from the input fields
            string nom = NomEntry.Text;
            string photo = PhotoEntry.Text;

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(photo))
            {
                await DisplayAlert("Validation Error", "Both fields are required.", "OK");
                return;
            }

            // Create a new category object
            var newCategory = new CategorieCreate
            {
                nom = nom,
                photo = photo
            };

            // Call the service to add the category

            bool result = _categorieService.AddCategorie(newCategory);
            if (result)
            {
                await DisplayAlert("Success", "Category added successfully!", "OK");
                await Navigation.PopAsync(); // Return to the previous page
            }
            else
            {
                await DisplayAlert("Error", "An error occurred while adding the category.", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Go back to the previous page
        }
    }
}
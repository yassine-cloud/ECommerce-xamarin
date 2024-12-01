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
    public partial class EditCategoriePage : ContentPage
    {
        private readonly CategorieService _categorieService;
        public CategorieUpdate Categorie { get; set; }

        public EditCategoriePage(CategorieRead categorie)
        {
            InitializeComponent();
            _categorieService = new CategorieService();

            // Initialize the Categorie property with the category data passed to the page
            Categorie = new CategorieUpdate
            {
                id = categorie.id,
                nom = categorie.nom,
                photo = categorie.photo
            };

            // Bind the data context to the Categorie object
            BindingContext = Categorie;
        }

        // Save the edited category
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            bool result = _categorieService.UpdateCategorie(Categorie);
            if (result)
            {
                await DisplayAlert("Success", "Category updated successfully.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "An error occurred while updating the category.", "OK");
            }
        }

        // Cancel the edit and go back to the previous page
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
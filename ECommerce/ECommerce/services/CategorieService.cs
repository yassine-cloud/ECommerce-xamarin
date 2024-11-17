using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using ECommerce.models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ECommerce.services
{
    public class CategorieService
    {

        private readonly HttpClient httpClient;
        public CategorieService()
        {
            httpClient = ApiService.GetHttpClient();   
        }

        public List<Categorie> GetCategories()
        {
            HttpResponseMessage response = httpClient.GetAsync("categorie/categories").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Categorie>>(jsonString);
            }
            return null;
        }

        public Categorie GetCategorie(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync($"categorie/category/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Categorie>(jsonString);
            }
            return null;
        }


    }
}
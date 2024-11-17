using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerce.models;
using Newtonsoft.Json;

namespace ECommerce.services
{
    public class ProduitService
    {
        private readonly HttpClient _httpClient;

        public ProduitService()
        {
            _httpClient = ApiService.GetHttpClient();
        }

        // Retrieve all products
        public List<Produit> GetProduits()
        {
            HttpResponseMessage response = _httpClient.GetAsync("produit/produits").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Produit>>(jsonString);
            }
            return null;
        }


        public List<Produit> GetProduits(string category)
        {
            HttpResponseMessage response = _httpClient.GetAsync("produit/produits").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var list = JsonConvert.DeserializeObject<List<Produit>>(jsonString);
                if (category != null)
                {
                    List<Produit> filteredList = new List<Produit>();
                    foreach (var produit in list)
                    {
                        if (produit.categorie.Equals(category))
                        {
                            filteredList.Add(produit);
                        }
                    }
                    return filteredList;
                }
            }
            return null;
        }

        public Produit GetProduit(int id)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"produit/produit/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Produit>(jsonString);
            }
            return null;
        }

    }
}

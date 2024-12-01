using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerce.auth;
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
            HttpResponseMessage response = _httpClient.GetAsync("produits").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Produit>>(jsonString);
            }
            return null;
        }


        public List<Produit> GetProduits(string categorieId)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"produits/categorie/{categorieId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var list = JsonConvert.DeserializeObject<List<Produit>>(jsonString);
                return list;
            }
            return null;
        }

        public Produit GetProduit(int id)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"produits/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Produit>(jsonString);
            }
            return null;
        }

        public bool AddProduit(ProduitCreate produit, string categoryId)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(produit), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync($"produits?categorieId={categoryId}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdateProduit(ProduitUpdate produit)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(produit), Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync($"produits/{produit.id}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool DeleteProduit(string id)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = _httpClient.DeleteAsync($"produits/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}

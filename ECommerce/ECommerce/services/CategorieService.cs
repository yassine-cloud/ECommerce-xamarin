using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using ECommerce.auth;
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

        public List<CategorieRead> GetCategories()
        {
            HttpResponseMessage response = httpClient.GetAsync("categories").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<CategorieRead>>(jsonString);
            }
            return null;
        }

        public CategorieRead GetCategorie(int categorieId)
        {
            HttpResponseMessage response = httpClient.GetAsync($"categorie/{categorieId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CategorieRead>(jsonString);
            }
            return null;
        }

        public bool AddCategorie(CategorieCreate categorie)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(categorie), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("categories", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdateCategorie(CategorieUpdate categorie)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(categorie), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PutAsync("categories", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool DeleteCategorie(int id)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.DeleteAsync($"categories/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


    }
}
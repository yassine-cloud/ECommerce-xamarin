using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ECommerce.auth;
using ECommerce.models;
using Newtonsoft.Json;

namespace ECommerce.services
{
    public class PanierService
    {
        private readonly HttpClient httpClient;
        public PanierService()
        {
            httpClient = ApiService.GetHttpClient();
        }

        public List<PanierRead> GetPaniers()
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync("paniers").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PanierRead>>(jsonString);
            }
            return null;
        }

        public PanierRead GetPanier(int panierId)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync($"paniers/{panierId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<PanierRead>(jsonString);
            }
            return null;
        }

        public List<PanierRead> GetUserPanier(string userId)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync($"paniers/user/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PanierRead>>(jsonString);
            }
            return null;
        }

        public bool AddPanier(PanierCreate panier)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }

            string request = $"paniers?userId={panier.userId}&produitId={panier.produitId}&quantite={panier.quantite}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(panier), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(request, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdatePanier(PanierUpdate panier)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }

            string request = $"paniers/{panier.id}?quantite={panier.quantite}";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(panier), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PutAsync(request, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool DeletePanier(int id)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.DeleteAsync($"paniers/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}

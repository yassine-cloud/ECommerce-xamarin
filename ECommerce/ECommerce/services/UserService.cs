using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ECommerce.auth;
using ECommerce.models;
using Newtonsoft.Json;

namespace ECommerce.services
{
    class UserService
    {
        private readonly HttpClient httpClient;

        public UserService()
        {
            httpClient = ApiService.GetHttpClient();
        }

        public List<UserRead> getAllUsers()
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync("users").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<UserRead>>(jsonString);
            }
            return null;
        }

        public UserRead getUserById(string id)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync($"users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UserRead>(jsonString);
            }
            return null;
        }

        public UserRead getUserByEmail(string email)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync($"users/email?email={email}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UserRead>(jsonString);
            }
            return null;
        }

        public bool updateUser(UserUpdate user)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PutAsync("users", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool deleteUser(string id)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.DeleteAsync($"users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool createUser(UserCreate user)
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("users", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<UserRead> readUsers() 
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return null;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync("users").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<UserRead>>(jsonString);
            }
            return null;
        }
    }
}

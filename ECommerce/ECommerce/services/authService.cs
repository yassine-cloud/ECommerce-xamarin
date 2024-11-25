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
    internal class authService
    {
        private readonly HttpClient httpClient;

        public authService()
        {
            httpClient = ApiService.GetHttpClient();
        }

        public bool VerifyToken()
        {
            var token = SessionManager.GetTokenAsync().Result;
            if (token == null)
            {
                return false;
            }
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = httpClient.GetAsync("auth").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public bool Login(string email, string password)
        {
            var data = new
            {
                email,
                password
            };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("auth/login", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);
                // Access the token and user information
                string token = loginResponse.token;
                UserRead user = loginResponse.user;

                SessionManager.SaveSession(token, user);

                // Example: Manipulate the user data or perform other logic
                Console.WriteLine($"Token: {token}");
                Console.WriteLine($"User ID: {user.id}");
                Console.WriteLine($"User Name: {user.nom} {user.prenom}");
                Console.WriteLine($"Role: {user.role}");

                return true;
            }
            return false;
        }

        public bool Register(UserCreate user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("auth/signup", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public void Logout()
        {
            SessionManager.ClearSession();
        }


    }
}

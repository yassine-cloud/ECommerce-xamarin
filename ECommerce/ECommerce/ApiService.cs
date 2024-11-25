using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace ECommerce
{
    public class ApiService
    {
        public static readonly string apiUrl = "http://10.0.2.2:8080/";
        private static HttpClient _httpClient;

        //public ApiService()
        //{
        //    _httpClient = new HttpClient
        //    {
        //        BaseAddress = new Uri("http://10.0.2.2:8080/")
        //    };
        //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        public static HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(apiUrl)
                };
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return _httpClient;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
namespace DealProject
{
    public static class GlobalVarables
    {
        public static HttpClient WebApiClient = new HttpClient();
       static GlobalVarables()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44396/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
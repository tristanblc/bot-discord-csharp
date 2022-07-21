using AutoMapper;
using BotClassLibrary.Package;
using Microsoft.Extensions.Configuration;
using ReaderClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Reader
{
    public class LaposteApiReader : ILaposteApi
    {
        private HttpClient HttpClient { get; }
        private string uri { get; set; }

        private IMapper Mapper { get; set; }

        public LaposteApiReader(HttpClient httpClient)
        {
            //Init mapper
            var config = new MapperConfiguration(cfg => {

            });
            Mapper = new Mapper(config);


            var AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("app.json")
                    .Build();

            HttpClient = httpClient;
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", AppSetting["bearer_api_consumer:token_laposte_api"]);
            HttpClient.DefaultRequestHeaders.Accept.Clear();

            uri = "https://api.laposte.fr/suivi/v2/idships/";

            HttpClient.BaseAddress = new Uri(uri);

        }



        public Task<ReturnMessage> Get(string idShip)
        {
            try
            {
                var url = HttpClient.BaseAddress.ToString() + idShip + "?lang = fr_FR";
                HttpClient.BaseAddress = new Uri(url);
                
                var resultat = HttpClient.GetFromJsonAsync<ReturnMessage>(uri);

                return resultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

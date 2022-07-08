using BotBlazonApplication.Services.Interface;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace BotBlazonApplication.Services.Service
{
    public class AuthService : IAuthService
    {

        private HttpClient HttpClient { get;set; }
        private IConfiguration Configuration { get; set; }
        private Users logUser { get; init; }
        

   

        public AuthService()
        {
            HttpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> Authentification(string email, string password)
        {


                BotClassLibrary.Users users = new BotClassLibrary.Users(email, password);

                var uri = $"https://localhost:7167/api/User/authenticate";

                StringContent content = new StringContent(
                          JsonConvert.SerializeObject(users),
                          Encoding.UTF8,
                         "application/json"
                );

                HttpRequestMessage resultat = new HttpRequestMessage
                {
                    RequestUri = new Uri(uri),
                    Content = content,
                    Method = HttpMethod.Post
                };

                try
                 {
                     HttpResponseMessage response = await HttpClient.SendAsync(resultat);
                     return response;

                 }
                catch(Exception ex)
                {
                      return null;
                 }
                 
        }
    }
}

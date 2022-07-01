using BotBlazonApplication.Services.Interface;


using System.Net;
using System.Net.Http.Json;

namespace BotBlazonApplication.Services.Service
{
    public class AuthService : IAuthService
    {

        private HttpClient HttpClient { get;set; }
        private  string uri { get; init; }
        private Users logUser { get; init; }

        public AuthService()
        {
            HttpClient = new HttpClient();
           

        }
        public async Task<HttpStatusCode> Authentification(string email, string password)
        {

            try
            {
                BotClassLibrary.Users users = new BotClassLibrary.Users(email,password);
            
                var uri =  "https://localhost:7167/api/User/authenticate";

                try
                {
                    var resultat = HttpClient.PostAsJsonAsync(uri, users).Result;
                    if (resultat.IsSuccessStatusCode)
                    {

                        return HttpStatusCode.OK;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                
              

                return HttpStatusCode.BadRequest;

            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }

        }
    }
}

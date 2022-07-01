using System.Net;

namespace BotBlazonApplication.Services.Interface
{
    public interface IAuthService
    {
        Task<HttpStatusCode> Authentification(string username, string password);    
    
    }
}

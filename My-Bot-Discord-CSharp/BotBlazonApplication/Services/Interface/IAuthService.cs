using System.Net;

namespace BotBlazonApplication.Services.Interface
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> Authentification(string username, string password);    
    
    }
}

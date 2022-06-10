using BotClassLibrary;

namespace ApiApplication.Authentification.Interface
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
       
        void CreateUser(Users user);
        
    }
}

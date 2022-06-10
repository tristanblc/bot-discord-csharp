namespace ApiApplication.Services
{
    public interface IPasswordHasherService
    {
        string GetPasswordHasher(string password_not_hashed);
    }
}

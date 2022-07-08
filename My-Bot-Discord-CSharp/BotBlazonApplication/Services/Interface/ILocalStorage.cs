namespace BotBlazonApplication.Services.Interface
{
    public interface ILocalStorage
    {
        Task RemoveAsync(string key);
        Task SaveStringAsync(string key, string value);
        Task<string> GetStringAsync(string key);
        Task SaveStringArrayAsync(string key, string[] values);
        Task<string[]> GetStringArrayAsync(string key);
    }
}

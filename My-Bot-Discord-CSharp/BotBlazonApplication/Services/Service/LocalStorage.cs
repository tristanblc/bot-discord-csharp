using BotBlazonApplication.Services.Classes;
using BotBlazonApplication.Services.Interface;
using Microsoft.JSInterop;
using System.IO.Compression;
using System.Text;

namespace BotBlazonApplication.Services.Service
{
    public class LocalStorage : ILocalStorage
    {
		private readonly IJSRuntime jsruntime;
		public LocalStorage(IJSRuntime jSRuntime)
		{
			jsruntime = jSRuntime;
		}

		public async Task RemoveAsync(string key)
		{
			await jsruntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
		}

		public async Task SaveStringAsync(string key, string value)
		{
			var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(value));
			await jsruntime.InvokeVoidAsync("localStorage.setItem", key, Convert.ToBase64String(compressedBytes)).ConfigureAwait(false);
		}

		public async Task<string> GetStringAsync(string key)
		{
			var str = await jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
			if (str == null)
				return null;
			var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
			return Encoding.UTF8.GetString(bytes);
		}

		public async Task SaveStringArrayAsync(string key, string[] values)
		{
			await SaveStringAsync(key, values == null ? "" : string.Join('\0', values));
		}

		public async Task<string[]> GetStringArrayAsync(string key)
		{
			var data = await GetStringAsync(key);
			if (!string.IsNullOrEmpty(data))
				return data.Split('\0');
			return null;
		}
	}

	
}

using System.IO.Compression;

namespace BotBlazonApplication.Services.Classes
{
	internal class Compressor
	{
		public static async Task<byte[]> CompressBytesAsync(byte[] bytes)
		{
			using (var outputStream = new MemoryStream())
			{
				using (var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal))
				{
					await compressionStream.WriteAsync(bytes, 0, bytes.Length);
				}
				return outputStream.ToArray();
			}
		}

		public static async Task<byte[]> DecompressBytesAsync(byte[] bytes)
		{
			using (var inputStream = new MemoryStream(bytes))
			{
				using (var outputStream = new MemoryStream())
				{
					using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
					{
						await compressionStream.CopyToAsync(outputStream);
					}
					return outputStream.ToArray();
				}
			}
		}
	}
}

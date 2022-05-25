using DSharpPlus.Entities;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Services
{
    public class FileService : IFileService
    {
        private WebClient WebClient{ get; init; }


        private string DirectoryForSave { get; init; } = Directory.GetCurrentDirectory();

        public FileService()
        {
              WebClient = new WebClient();
        }

        public void SaveFile(string fileUrl, string filename)
        {
            
            var stream = WebClient.OpenRead(fileUrl);

            

            var path = Path.Combine(DirectoryForSave, "documents");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            stream.Seek(0, SeekOrigin.Begin);

            using (var fs = new FileStream("path", FileMode.OpenOrCreate))
            {
                stream.CopyTo(fs);
            }
            stream.Dispose();




        }




        public void DeleteFile(string filename)
        {
            
            var path = Path.Combine(DirectoryForSave, "documents");
            var pathBitmap = Path.Combine(path, filename);

            File.Delete(pathBitmap);
        }

        public void WriteTxt(List<DiscordMessage> messages,string filename)
        {
            var path = Path.Combine(DirectoryForSave, "documents");
            var pathFile = Path.Combine(path, filename);
            using (StreamWriter writetext = new StreamWriter(Path.Join(pathFile)))
            {

                messages.ToList().ForEach(async message => writetext.WriteLine(message.Content.ToString()));

            }

        }
    }
}

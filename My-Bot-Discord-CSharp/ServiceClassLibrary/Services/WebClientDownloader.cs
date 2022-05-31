using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class WebClientDownloader : IWebClientDownloader
    {
        private WebClient WebClient { get; set; }

        private string PathToSave { get; set; }



        public WebClientDownloader(WebClient webClient)
        {
            WebClient = webClient;

            PathToGet = Directory.GetCurrentDirectory() + "video";


        }

        public void DownloadVideoFromDiscord(DiscordAttachment discordAttachement)
        {

            var path = Path.Join(PathToSave, discordAttachement.FileName);
            try
            {

                WebClient.DownloadFileAsync(new Uri(discordAttachement.Url),path);
               

            }
            catch(Exception ex)
            {
                throw new FileDownloadException($"Erreur dans le téléchargement du fichier {discordAttachement.FileName}");

            }
      
        }
    }
}

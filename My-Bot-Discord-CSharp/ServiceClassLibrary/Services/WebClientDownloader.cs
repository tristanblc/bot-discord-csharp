using DSharpPlus.Entities;
using ExceptionClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IUtilsService UtilsService { get; set; }

        public WebClientDownloader(WebClient webClient)
        {
            WebClient = webClient;
            PathToSave = Path.Join (Directory.GetCurrentDirectory() , "video");
            UtilsService = new UtilsService();

        }

        public void DownloadVideoFromDiscord(DiscordAttachment discordAttachement)
        {
            UtilsService.DeleteDirectoryIfExist(PathToSave);
            var path = Path.Join(PathToSave, discordAttachement.FileName);
            try
            {
                WebClient.DownloadFile(new Uri(discordAttachement.Url), path);
                WebClient.Dispose();
            }
            catch(Exception ex)
            {
                throw new FileDownloadException($"Erreur dans le téléchargement du fichier {discordAttachement.FileName}");
            }       
        }        
    }
}

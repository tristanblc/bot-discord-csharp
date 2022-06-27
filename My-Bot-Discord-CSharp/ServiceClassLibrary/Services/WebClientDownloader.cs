using DSharpPlus.Entities;
using ExceptionClassLibrary;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
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


        private ILoggerProject LoggerProject { get; init; }

        private HtmlDocument HtmlDoc { get; init; }


        public WebClientDownloader(WebClient webClient)
        {
            WebClient = webClient;
            PathToSave = Path.Join (Directory.GetCurrentDirectory() , "video");
            UtilsService = new UtilsService();
            LoggerProject = new LoggerProject();
            HtmlDoc =  new HtmlDocument();

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

                LoggerProject.WriteLogErrorLog($"Erreur dans le téléchargement du fichier {discordAttachement.FileName}");
                throw new FileDownloadException($"Erreur dans le téléchargement du fichier {discordAttachement.FileName}");
            }       
        }

        public void DownloadVideo(string url,string name)
        {
            UtilsService.DeleteDirectoryIfExist(PathToSave);
            var path = Path.Join(PathToSave, name );
            try
            {
                WebClient.DownloadFile(new Uri(url), path);
                WebClient.Dispose();
            }
            catch (Exception ex)
            {

                LoggerProject.WriteLogErrorLog($"Erreur dans le téléchargement du fichier {name}");
                throw new FileDownloadException($"Erreur dans le téléchargement du fichier {name}");
            }
        }

        public FileStream ConvertVideoToStream(string path)
        {

            try
            {
                return File.Open(path, FileMode.Open);
            }
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"can't convert ot filestream video -> path = {path} ");
                throw new FileDownloadException("Exception");
            }
           
        }

     
    }
}

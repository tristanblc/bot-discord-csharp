using DSharpPlus.Entities;
using ExceptionClassLibrary;
using HtmlAgilityPack;
using ModuleBotClassLibrary.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PdfSharp.Pdf;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServiceClassLibrary.Services
{
    public class ScreenerSite : IScreenerSite
    {
        public IUtilsService UtilsService { get; init; }

        public IFileService FileService { get; init; }
        private string DirectoryForSave { get; init; } = Path.Join(Directory.GetCurrentDirectory(), "documents");
        public ScreenerSite()
        {
            UtilsService = new UtilsService();
            FileService = new FileService();
        }
        public DiscordEmbedBuilder MakeFileOfSite(string url)
        {

            try
            {
                string Url = url;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(Url);

                var docname = $"pdf_test.html";

                try
                {

                    FileService.SavePDFFile(doc, docname);
                }
                catch (Exception ex) {


                    throw new FileDownloadException("Exception");                
                
                }


                var message = UtilsService.CreateNewEmbed("Screenshot", DiscordColor.Azure, $"Take screenshot");
           

                return message;

            }
            catch(FileDownloadException fileexception)
            {
                throw new FileDownloadException($"Error : can't save file");
            } 
             catch(Exception ex)
            {
                throw new Exception($"Error : can't take a screenshot from url :{ url }");
            }
          
        }
    }
}

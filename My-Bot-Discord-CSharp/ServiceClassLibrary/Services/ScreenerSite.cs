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

        private ILoggerProject LoggerProject { get; init; }
        private string DirectoryForSave { get; init; } = Path.Join(Directory.GetCurrentDirectory(), "documents");
        public ScreenerSite()
        {
            UtilsService = new UtilsService();
            FileService = new FileService();
            LoggerProject = new LoggerProject();
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

                    var exception_message = "cannot convert to HTML File";
                    LoggerProject.WriteLogErrorLog(exception_message);
      

                    throw new FileDownloadException(exception_message);                
                
                }


                var message = UtilsService.CreateNewEmbed("Screenshot", DiscordColor.Azure, $"Take screenshot");
           

                return message;

            }
            catch(FileDownloadException fileexception)
            {
                var exception_message = "cannot save file";
                LoggerProject.WriteLogErrorLog(exception_message);


                throw new FileDownloadException(exception_message);
    
            } 
             catch(Exception ex)
            {
                var exception_message = $"Error : can't take a screenshot from url :{url}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new Exception(exception_message);
            }
          
        }
    }
}

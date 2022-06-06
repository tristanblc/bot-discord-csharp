using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Services
{
    public class FileService : IFileService
    {
        private WebClient WebClient{ get; init; }


        private string DirectoryForSave { get; init; } = Directory.GetCurrentDirectory();


        private ILoggerProject LoggerProject { get; set; }

        private IUtilsService UtilsService { get; set; }

        public FileService()
        {
              WebClient = new WebClient();
              UtilsService = new UtilsService();
              LoggerProject = new LoggerProject();
        }

        public void SaveFile(string fileUrl, string filename)
        {

            try
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
            catch(Exception ex)
            {

                LoggerProject.WriteLogErrorLog("Probleme fichier -> soit load soit filestream");
                throw new FileLoadException("Error");
            }
            
         




        }




        public void DeleteFile(string filename)
        {
            try
            {
                var path = Path.Combine(DirectoryForSave, "documents");
                var pathBitmap = Path.Combine(path, filename);

                File.Delete(pathBitmap);
            }
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme delete fichier {filename}");
            }
            
           
        }

        public void WriteTxt(List<DiscordMessage> messages,string filename)
        {
            try
            {
                var path = Path.Combine(DirectoryForSave, "documents");
                var pathFile = Path.Combine(path, filename);
                using (StreamWriter writetext = new StreamWriter(Path.Join(pathFile)))
                {
                    try
                    {
                        messages.ToList().ForEach(async message => writetext.WriteLine(message.Content.ToString()));
                    }
                    catch (Exception ex)
                    {
                        LoggerProject.WriteLogWarningLog($"Probleme message writetxt");

                    }

            

                }
            }
           
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme write fichier {filename}");
            }

        }
        public void WriteJson(List<DiscordMessage> messages, string filename)
        {
            try
            {
                var path = Path.Combine(DirectoryForSave, "documents");
                var pathFile = Path.Combine(path, filename);
                using (StreamWriter writetext = new StreamWriter(Path.Join(pathFile)))
                {
                    try
                    {
                        messages.ToList().ForEach(async message =>
                        {
                            //if (UtilsService.isJson(message.ToString()) == true)                 
                            writetext.WriteLine(JsonConvert.SerializeObject(message.ToString()));
                        });
                    }
                    catch (Exception ex)
                    {
                        LoggerProject.WriteLogErrorLog($"Probleme message writejson conversion json");
                    }

                }
            }
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme write fichier json {filename}");
            }
          

        }

        public string Compress2Zip(List<DiscordAttachment> attachements, string filename)
        {
           

            
            var path = Path.Combine(DirectoryForSave, "zip_document");
            var path_directory = Path.Combine(path, "zip_document_file");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            if (!Directory.Exists(path_directory))
                Directory.CreateDirectory(path_directory);


            foreach (var attachment in attachements)
            {
                try
                {
                    var pathFile = Path.Combine(path_directory, attachment.FileName.ToString());

                    WebClient.DownloadFile(attachment.Url, pathFile);
                }
                catch(Exception ex)
                {
                    LoggerProject.WriteLogErrorLog($"Probleme webclient");
                }
            }

            

             

            

            var path_zip = Path.Combine(path, $"{filename}.zip");

            if (File.Exists(path_zip))
                File.Delete(path_zip);

            try
            {              

                ZipFile.CreateFromDirectory(path_directory, path_zip);
        
            }
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme zip conversion");
                throw new FileNotFoundException("Error ");
            }


            Directory.GetFiles(path_directory).ToList().ForEach(file => File.Delete(file));

            return path_zip;
        }

        public List<string> Decompress2File(DiscordAttachment attachment)
        {

            var path = Path.Combine(DirectoryForSave, "extract_zip_document");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            if (Directory.GetFiles(path).Length > 0) {
                Directory.GetFiles(path).ToList().ForEach(file => File.Delete(file));
            }


            var stream = WebClient.OpenRead(attachment.Url);
            var pathFile = Path.Combine(path, attachment.FileName);



            try
            {
           
                WebClient.DownloadFile(attachment.Url, pathFile);
            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme webclient extract zip");
            }


            try
            {

                ZipFile.ExtractToDirectory(pathFile, path);
            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Probleme zip extract");
                throw new FileLoadException("Error zip");
            }



            return Directory.GetFiles(path).ToList();
        }

    
    }
}

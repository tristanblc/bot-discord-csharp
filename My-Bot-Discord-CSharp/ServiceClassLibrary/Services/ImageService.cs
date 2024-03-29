﻿using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Services
{
    public class ImageService :  IImageService
    {
        private WebClient WebClient{ get; init; }
        private ILoggerProject LoggerProject { get; init; }

        private string DirectoryForSave { get; init; } = Directory.GetCurrentDirectory();

        public ImageService()
        {
              WebClient = new WebClient();
                 LoggerProject = new LoggerProject(); 
        }

        public void SaveImage(string imageUrl, string filename)
        {

                Stream stream = WebClient.OpenRead(imageUrl);
                Bitmap bitmap; bitmap = new Bitmap(stream);
       
 


            var path = Path.Combine(DirectoryForSave, "images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathBitmap = Path.Combine(path, filename);

            if (bitmap != null)
            {           
                bitmap.Save(pathBitmap,ImageFormat.Png);
                bitmap.Dispose();
            }
           

            stream.Flush();
            stream.Close();
            WebClient.Dispose();
           
            
        }
        public void SaveImage(Bitmap image,string path)
        {
            try
            {
                if (image != null)
                {
                    image.Save(path, ImageFormat.Png);


                }

            }
            catch(Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Bitmap save");

            }
           
                


        }
        public void DeleteImage(string filename)
        {

            try
            {
                var path = Path.Combine(DirectoryForSave, "images");

                var pathBitmap = Path.Combine(path, filename);

                File.Delete(pathBitmap);


            }
            catch (Exception ex)
            {
                LoggerProject.WriteLogErrorLog($"Error Bitmap delete");

            }
         
        }
    }
}

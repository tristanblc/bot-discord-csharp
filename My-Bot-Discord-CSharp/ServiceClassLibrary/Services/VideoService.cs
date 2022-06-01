using DSharpPlus.Entities;
using ExceptionClassLibrary;
using FFMpegCore;
using FFMpegCore.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class VideoService : IVideoService
    {

        private IWebClientDownloader _downloader { get; set; }

        private string PathGetVideo { get; set; } = Path.Join(Directory.GetCurrentDirectory(),"video");

        private string PathVideoAudio { get; set; } = Path.Join(Directory.GetCurrentDirectory(),"extract_audio");


        private IUtilsService UtilsService { get; set; }

        public VideoService()
        {
            _downloader = new WebClientDownloader(new System.Net.WebClient());
            UtilsService = new UtilsService();
   
        }

        public string CompressVideo(DiscordAttachment discordAttachement)
        {
            try
            {
              _downloader.DownloadVideoFromDiscord(discordAttachement);
               var path = Path.Join(PathGetVideo, discordAttachement.FileName);
                var output_path = Path.Join(PathGetVideo, $"extract_{discordAttachement.FileName}");


                try
                {
                   FFMpegArguments
                        .FromFileInput(path)
                        .OutputToFile(output_path)
                        .ProcessAsynchronously(true);
                }
                catch(Exception ex)
                {
                    throw new VideoException("Error => Erreur enregistrement");

                }

                return output_path;
            }
            catch (Exception ex)
            {
                throw new VideoException($"Error :  can't compress video ");
            }
            return "";
        }

      
        public void DeleteVideo(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch(Exception ex)
            {
                throw new VideoException($"Error :  can't delete video from path { path }");
            }
        }

        public string GetVideoInfo(DiscordAttachment discordAttachment)
        {
            try
            {
                var path = Path.Join(PathGetVideo, discordAttachment.FileName);

                _downloader.DownloadVideoFromDiscord(discordAttachment);

                var result = FFProbe.Analyse(path);

                return $" Name : {discordAttachment.FileName  } - Size {discordAttachment.FileSize} - Format :{ result.Format} - duration : {result.Duration}";
            
            }
            catch (Exception ex)
            {
                throw new VideoException($"Error :  can't compress video ");
            }

            return null;
        }

      
        public string ExtractAudioFromVideo(DiscordAttachment discordAttachment)
        {
           
            try
            {
                    var path = Path.Join(PathGetVideo, discordAttachment.FileName);    

                    _downloader.DownloadVideoFromDiscord(discordAttachment);

                    var path_filename = Path.Join(PathGetVideo, discordAttachment.FileName);
             

                    var PathVideoAudio_extract = Path.Join(PathVideoAudio, Path.Join(discordAttachment.FileName + FileExtension.Mp3));
                    UtilsService.DeleteFile(PathVideoAudio_extract);

                    UtilsService.DeleteDirectoryIfExist(PathVideoAudio);
                    FFMpeg.ExtractAudio(path_filename, PathVideoAudio_extract);

                    return PathVideoAudio_extract;
                           


            }
            catch (Exception ex)
            {
                throw new VideoException($"Error :  can't compress video ");
            }

            return null;
        }

        
        public void UploadVideoOnDirectory(DiscordAttachment discordAttachment)
        {
            try{
                _downloader.DownloadVideoFromDiscord(discordAttachment);
            }
            catch(Exception ex)
            {
                throw new VideoException("Error upload");
            }
        }
    }
}

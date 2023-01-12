using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ModuleBotClassLibrary.RessourceManager;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public  class VideoModule : BaseCommandModule
    {
        private IVideoService VideoService { get; set; }
        private IUtilsService UtilsService { get; set; }

        public VideoModule()
        {
            VideoService = new VideoService();
            UtilsService = new UtilsService();

        }

        [Command("uploadvideo")]
        [DescriptionCustomAttribute("uploadCommand")]
        public async Task HandleUploadVideoCommand(CommandContext ctx)
        {
            var builder = UtilsService.CreateNewEmbed("Status", DiscordColor.Azure, "");

            ctx.Message.Attachments.ToList().ForEach(attachment =>
            {
                var builder = UtilsService.CreateNewEmbed("Status", DiscordColor.Red, "");
                try
                {
                    VideoService.UploadVideoOnDirectory(attachment);
                }
                catch(Exception ex)
                {
                    builder.Description = ex.ToString();

                }

            });
            await ctx.RespondAsync(builder.Build());
        }

        [Command("extractaudio")]
        [DescriptionCustomAttribute("extractCommand")]
        public async Task HandleExtractAudioCommand(CommandContext ctx)
        {
            var builder = UtilsService.CreateNewEmbed("Audio", DiscordColor.Green, "send audio");


            List<string> audios = new List<string>();

            ctx.Message.Attachments.ToList().ForEach(attachment =>
            {
               
                try
                {
                    audios.Add(VideoService.ExtractAudioFromVideo(attachment));
                }
                catch (Exception ex)
                {
                    builder.Description = ex.ToString();

                }

            });


            await ctx.RespondAsync(builder.Build());

            audios.ForEach(path =>
            {
                DiscordMessageBuilder builders = UtilsService.SendImage(path);
                builders.SendAsync(ctx.Channel);
            });



  
        }

        [Command("compressvideo")]
        [DescriptionCustomAttribute("compressCommand")]
        public async Task HandleCompressVideoCommand(CommandContext ctx)
        {
     


            List<string> audios = new List<string>();
            var builder = UtilsService.CreateNewEmbed("Compress video", DiscordColor.Red, "Send compress video");

            ctx.Message.Attachments.ToList().ForEach(attachment =>
            {

                try
                {
                    audios.Add(VideoService.CompressVideo(attachment));
                }
                catch (Exception ex)
                {
                    builder.Description = ex.ToString();

                }

            });

            await ctx.RespondAsync(builder.Build());

            audios.ForEach(path =>
            {

                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                try
                {

                    FileStream fileStream = VideoService.GetStream(path);   
                    builders.AddFile(fileStream);


                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

              
         
            
                builders.SendAsync(ctx.Channel);
            });

        }

        [Command("videoinfo")]
        [DescriptionCustomAttribute("metadataCommand")]
        public async Task HandleSVideoInfoCommand(CommandContext ctx)
        {
            var builder = UtilsService.CreateNewEmbed("Video info", DiscordColor.Green, "Info of videos");

     

            List<string> audios = new List<string>();

            ctx.Message.Attachments.ToList().ForEach(attachment =>
            {

                try
                {
                    builder.Description += VideoService.GetVideoInfo(attachment) + "\n";

                }
                catch (Exception ex)
                {
                    builder.Description += ex.ToString();

                }

            });


            await ctx.RespondAsync(builder.Build());


        }


    }
}

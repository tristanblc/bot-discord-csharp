using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
        public async Task HandleCompressVideoCommand(CommandContext ctx)
        {
            var builder = UtilsService.CreateNewEmbed("Compress video", DiscordColor.Red, "send audio");


            List<string> audios = new List<string>();

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


                DiscordMessageBuilder builders = UtilsService.SendImage(path);
                builders.SendAsync(ctx.Channel);
            });

        }

       
        
        [Command("videoinfo")]
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

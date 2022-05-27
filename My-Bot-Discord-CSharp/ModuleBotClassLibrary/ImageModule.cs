using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{

    public class ImageModule : BaseCommandModule
    {

        private ImageService imageService { get; set; }

        public ImageModule()
        {
            imageService = new ImageService();

        }

        [Command("transpiracy")]
        public async Task HandleImageToTranspiracy(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach(var attachment in attachments)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");
                
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
             
                var pathFile = Path.Join(path, "changed.png");

                if(File.Exists(pathFile))
                    File.Delete(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, "changed.png");

                }
                
                catch(Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }    


                IEffectImageService effectImageService = new EffectImageService();


                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = Path.Join(path, "image_filter_changed.png");

                imageService.SaveImage(effectImageService.DrawWithTransparency(bitmap),pathFileFilter);

                var builder = new DiscordEmbedBuilder
                {
                    Title = "Add filter to image",

                    Color = DiscordColor.Azure,
                    Description = "Add filter ..."
                };


                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                FileStream fileStream = new FileStream(pathFileFilter, FileMode.Open);
                builders.WithFile(fileStream);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);
            }


           
        }

        [Command("grayscale")]
        public async Task HandleImageToGrayscale(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var pathFile = Path.Join(path, "changed.png");

                if (File.Exists(pathFile))
                    File.Delete(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, "changed.png");

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }


                IEffectImageService effectImageService = new EffectImageService();


                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = Path.Join(path, "image_filter_changed.png");

                imageService.SaveImage(effectImageService.DrawAsGrayscale(bitmap), pathFileFilter);

                var builder = new DiscordEmbedBuilder
                {
                    Title = "Add filter to image",

                    Color = DiscordColor.Azure,
                    Description = "Add filter ..."
                };


                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                FileStream fileStream = new FileStream(pathFileFilter, FileMode.Open);
                builders.WithFile(fileStream);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);
            }



        }

        [Command("sepiaTone")]
        public async Task HandleImageToSepiaTone(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var pathFile = Path.Join(path, "changed.png");

                if (File.Exists(pathFile))
                    File.Delete(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, "changed.png");

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }


                IEffectImageService effectImageService = new EffectImageService();


                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = Path.Join(path, "image_filter_changed.png");

                imageService.SaveImage(effectImageService.DrawAsSepiaTone(bitmap), pathFileFilter);

                var builder = new DiscordEmbedBuilder
                {
                    Title = "Add filter to image",

                    Color = DiscordColor.Azure,
                    Description = "Add filter ..."
                };


                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                FileStream fileStream = new FileStream(pathFileFilter, FileMode.Open);
                builders.WithFile(fileStream);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);
            }



        }
      

        [Command("negative")]
        public async Task HandleImageToNegativeCopy(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var pathFile = Path.Join(path, "changed.png");

                if (File.Exists(pathFile))
                    File.Delete(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, "changed.png");

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }


                IEffectImageService effectImageService = new EffectImageService();


                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = Path.Join(path, "image_filter_changed.png");

                imageService.SaveImage(effectImageService.DrawAsNegative(bitmap), pathFileFilter);

                var builder = new DiscordEmbedBuilder
                {
                    Title = "Add filter to image",

                    Color = DiscordColor.Azure,
                    Description = "Add filter ..."
                };


                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                FileStream fileStream = new FileStream(pathFileFilter, FileMode.Open);
                builders.WithFile(fileStream);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);
            }



        }

        [Command("argbCopy")]
        public async Task HandleImageToArgbCopy(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var pathFile = Path.Join(path, "changed.png");

                if (File.Exists(pathFile))
                    File.Delete(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, "changed.png");

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }


                IEffectImageService effectImageService = new EffectImageService();


                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = Path.Join(path, "image_filter_changed.png");

                imageService.SaveImage(effectImageService.GetArgbCopy(bitmap), pathFileFilter);

                var builder = new DiscordEmbedBuilder
                {
                    Title = "Add filter to image",

                    Color = DiscordColor.Azure,
                    Description = "Add filter ..."
                };


                DiscordMessageBuilder builders = new DiscordMessageBuilder();
                FileStream fileStream = new FileStream(pathFileFilter, FileMode.Open);
                builders.WithFile(fileStream);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);
            }



        }

    }
}

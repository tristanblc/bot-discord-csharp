using DSharpPlus;
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


        private string path { get; init; } = Path.Combine(Directory.GetCurrentDirectory(), "images");
        private IUtilsService utilsService { get; set; }

        private IEffectImageService effectImageService { get; set; }
        private ImageService imageService { get; set; }

        public ImageModule()
        {
            imageService = new ImageService();
            effectImageService= new EffectImageService();
            utilsService = new UtilsService();
            utilsService.CheckDirectory(path);

        }

   
        [Command("transpiracy")]
        public async Task HandleImageToTranspiracy(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {

                utilsService.DeleteDirectoryIfExist(path);

                var pathFile = Path.Join(path, attachment.FileName);

                utilsService.DeleteFile(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, attachment.FileName);

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }

                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = utilsService.GetFilePathFilter(path,attachment);


                utilsService.DeleteFile(pathFileFilter);

                imageService.SaveImage(effectImageService.DrawAsGrayscale(bitmap), pathFileFilter);
                utilsService.SendResultat(ctx, pathFileFilter);


            }
        }

        [Command("grayscale")]
        public async Task HandleImageToGrayscale(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {

                utilsService.DeleteDirectoryIfExist(path);

                var pathFile = Path.Join(path, attachment.FileName);

                utilsService.DeleteFile(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, attachment.FileName);

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }

                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = utilsService.GetFilePathFilter(path, attachment);


                utilsService.DeleteFile(pathFileFilter);

                imageService.SaveImage(effectImageService.DrawAsGrayscale(bitmap), pathFileFilter);
                utilsService.SendResultat(ctx, pathFileFilter);


            }



        }

        [Command("sepia")]
        public async Task HandleImageToSepiaTone(CommandContext ctx)
        {
            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {

                utilsService.DeleteDirectoryIfExist(path);

                var pathFile = Path.Join(path, attachment.FileName);

                utilsService.DeleteFile(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, attachment.FileName);

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }

                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = utilsService.GetFilePathFilter(path, attachment);


                utilsService.DeleteFile(pathFileFilter);

                imageService.SaveImage(effectImageService.DrawAsSepiaTone(bitmap), pathFileFilter);
                utilsService.SendResultat(ctx, pathFileFilter);


            }

        }


        [Command("negative")]
        public async Task HandleImageToNegativeCopy(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {

                utilsService.DeleteDirectoryIfExist(path);

                var pathFile = Path.Join(path, attachment.FileName);

                utilsService.DeleteFile(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, attachment.FileName);

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }

                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = utilsService.GetFilePathFilter(path, attachment);


                utilsService.DeleteFile(pathFileFilter);

                imageService.SaveImage(effectImageService.DrawAsNegative(bitmap), pathFileFilter);
                utilsService.SendResultat(ctx, pathFileFilter);


            }
        }

        [Command("bw")]
        public async Task HandleImageToBlackAndWhite(CommandContext ctx)
        {

            var attachments = ctx.Message.Attachments;

            foreach (var attachment in attachments)
            {

                utilsService.DeleteDirectoryIfExist(path);

                var pathFile = Path.Join(path, attachment.FileName);

                utilsService.DeleteFile(pathFile);

                try
                {
                    imageService.SaveImage(attachment.Url, attachment.FileName);

                }

                catch (Exception ex)
                {
                    await ctx.RespondAsync("Error");
                }

                Bitmap bitmap = new Bitmap(pathFile);


                var pathFileFilter = utilsService.GetFilePathFilter(path, attachment);


                utilsService.DeleteFile(pathFileFilter);

                imageService.SaveImage(effectImageService.DrawBlackAndWhite(bitmap), pathFileFilter);
                utilsService.SendResultat(ctx, pathFileFilter);


            }
        }



       


        [Command("clear-directory")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleClearDirectory(CommandContext ctx)
        {
            var builder = utilsService.CreateNewEmbed("Delete files in images Directory", DiscordColor.Azure, "Finished");
            try
            {
                utilsService.DeleteDirectoryIfExist(path);
                
                var paths = Directory.GetFiles(path);
                paths.ToList().ForEach(chemin => File.Delete(chemin));
                await ctx.RespondAsync(builder.Build());
            }
            catch(Exception ex)
            {
                builder.Color = DiscordColor.Red;
                builder.Description = "Error";
                await ctx.RespondAsync(builder.Build());
            }


               
        }

        [Command("watch-directory")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleWatchDirectory(CommandContext ctx)
        {
            string reponse = $"";

            var builder = utilsService.CreateNewEmbed("File in directory ", DiscordColor.Azure, "Empty");
            try
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

                utilsService.DeleteDirectoryIfExist(path);


                var files = Directory.GetFiles(path);

                foreach (var item in files)
                {
                    var file = new FileInfo(Path.Join(item));
                    reponse += $" Name : {file.Name} - Extension : {file.Extension} - LastWriteTime : {file.LastWriteTime.ToString()} -  Length :{file.Length} \n";
                }


                builder.Description = reponse;
       
                await ctx.RespondAsync(builder.Build());
            }
            catch (Exception ex)
            {
                builder.Color = DiscordColor.Red;
                builder.Description = "Error";
                await ctx.RespondAsync(builder.Build());
            }



        }
    }
}

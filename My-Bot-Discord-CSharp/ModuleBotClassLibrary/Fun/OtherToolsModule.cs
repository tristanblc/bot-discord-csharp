using AutoMapper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

using ExceptionClassLibrary;
using ModuleBotClassLibrary.RessourceManager;
using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class OtherToolsModule : BaseCommandModule
    {


        private IUtilsService utilsService { get; init; }
        private IScreenerSite ScreenerSite { get; init; }

        private ILaposteApi LaposteService { get; init; }

        private string DirectoryForSave { get; init; } = Path.Join(Directory.GetCurrentDirectory(), "documents");

        public OtherToolsModule()
        {
            utilsService = new UtilsService();
            ScreenerSite = new ScreenerSite();
            LaposteService = new LaposteService(new HttpClient());
        }


        [Command("random")]
        [DescriptionCustomAttribute("randomCommand")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var random = new Random();
            var builder = utilsService.CreateNewEmbed($"Random number between {min.ToString()} and {max.ToString()}", DiscordColor.Azure, $"{random.Next(min, max)}");
            await ctx.RespondAsync(builder.Build());

        }


        [Command("avatar")]
        [DescriptionCustomAttribute("avatarCommand")]
        public async Task AvatarCommand(CommandContext ctx, DiscordMember member)
        {

            var url = member.GetAvatarUrl(DSharpPlus.ImageFormat.Auto);
            await ctx.RespondAsync(url);
        }

        [Command("site2html")]
        [DescriptionCustomAttribute("siteCmd")]
        public async Task ScreenCommand(CommandContext ctx, string url)
        {
            try
            {

                var docname = $"pdf_test.html";


                var message = ScreenerSite.MakeFileOfSite(url);
                await ctx.RespondAsync(message.Build());


                var path = Path.Join(DirectoryForSave, docname);
                DiscordMessageBuilder discordMessage = utilsService.SendImage(path);
             

                discordMessage.SendAsync(ctx.Channel);
            }
            catch(FileDownloadException exe)
            {
                await ctx.RespondAsync("File downloader error");
            }
           
           
        }


        [Command("duck")]
        [DescriptionCustomAttribute("duckCmd")]
        public async Task DuckCommand(CommandContext ctx)
        {


            var service = new DuckService(new HttpClient(), "https://random-d.uk/api/v2/random");
            var builder = utilsService.CreateNewEmbed("Animal", DiscordColor.Azure, $"");
            try
            {
                var animal = await service.Get();
                builder.Description = animal.url.ToString();
            }
         
            catch (Exception ex)
            {
                builder.Description = "error";
                builder.Color = DiscordColor.Red;

            }
            await ctx.RespondAsync("erreur");
        }




        [Command("dog")]
        [DescriptionCustomAttribute("dogCmd")]
        public async Task DogCommand(CommandContext ctx)
        {


            var service = new DogService(new HttpClient(), "https://random.dog/woof.json");

            var builder = utilsService.CreateNewEmbed("Animal", DiscordColor.Azure, $"");
            try
            {
                var animal = await service.Get();

                builder.Description = animal.url.ToString();
               
            }
            catch (Exception ex)
            {
                builder.Description = "error";
                builder.Color = DiscordColor.Red;

            }
            await ctx.RespondAsync(builder.Build());
        }
        
        [Command("contribute")]
        [DescriptionCustomAttribute("cntCmd")]
        public async Task ContributeCommand(CommandContext ctx)
        {
            var url = "https://github.com/tristanblc/bot-discord-csharp";

            var builder = utilsService.CreateNewEmbed("Contribute", DiscordColor.Azure, $"Viens contribute sur git bg {url}");
          

            await ctx.RespondAsync(builder.Build());



        }


        [Command("laposte")]
        [DescriptionCustomAttribute("laposteCmd")]
        public async Task TrackPackageCommand(CommandContext ctx, string idShip)
        {

            try
            {
                var reponse = LaposteService.Get(idShip);
                var response = reponse.Result; 

                await ctx.RespondAsync(response.ToString());
            }
            catch(Exception ex)
            {
                await ctx.RespondAsync("ok");
            }
           
       



        }

    }
}

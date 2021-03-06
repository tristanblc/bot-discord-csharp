using AutoMapper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
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
        public OtherToolsModule()
        {
            utilsService = new UtilsService();
            ScreenerSite = new ScreenerSite();
            LaposteService = new LaposteService(new HttpClient());
        }


        [Command("random")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var random = new Random();
            var builder = utilsService.CreateNewEmbed($"Random number between {min.ToString()} and {max.ToString()}", DiscordColor.Azure, $"{random.Next(min, max)}");
            await ctx.RespondAsync(builder.Build());

        }


        [Command("avatar")]
        public async Task AvatarCommand(CommandContext ctx, DiscordMember member)
        {

            var url = member.GetAvatarUrl(DSharpPlus.ImageFormat.Auto);
            await ctx.RespondAsync(url);
        }

        [Command("screensite")]
        public async Task ScreenCommand(CommandContext ctx, string url)
        {

            var message = ScreenerSite.MakeFileOfSite(url);
            message.SendAsync(ctx.Channel);
        }


        [Command("duck")]
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
        public async Task ContributeCommand(CommandContext ctx)
        {
            var url = "https://github.com/tristanblc/bot-discord-csharp";

            var builder = utilsService.CreateNewEmbed("Contribute", DiscordColor.Azure, $"Viens contribute sur git bg {url}");
          

            await ctx.RespondAsync(builder.Build());



        }

        [Command("laposte")]
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

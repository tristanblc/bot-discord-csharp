using AutoMapper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using ReaderClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class OtherToolsModule : BaseCommandModule
    {
       
       [Command("random")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var random = new Random();
            await ctx.RespondAsync($"Le chiffre est : {random.Next(min, max)}");

        }


        [Command("avatar")]
        public async Task AvatarCommand(CommandContext ctx, DiscordMember member)
        {

            var url = member.GetAvatarUrl(DSharpPlus.ImageFormat.Auto);
            await ctx.RespondAsync(url);
        }

        [Command("duck")]
        public async Task DuckCommand(CommandContext ctx)
        {


            var service = new DuckService(new HttpClient(), "https://random-d.uk/api/v2/random");

            try
            {
                var animal = await service.Get();

                await ctx.RespondAsync(animal.url.ToString());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }
        }

       
   

        [Command("dog")]
        public async Task DogCommand(CommandContext ctx)
        {


            var service = new DogService(new HttpClient(), "https://random.dog/woof.json");

            try
            {
                var animal = await service.Get();

                await ctx.RespondAsync(animal.url.ToString());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }    
          
        }











    }
}

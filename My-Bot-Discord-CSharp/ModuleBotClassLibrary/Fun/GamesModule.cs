using DSharpPlus;
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
    public  class GamesModule : BaseCommandModule
    {

        private IUtilsService utilsService { get; set; }

        public GamesModule()
        {
            utilsService = new UtilsService();
     

        }

        [Command("pileouface")]
        public async Task PileOuFaceCommand(CommandContext ctx, string choice)
        {


            if (choice.ToString().ToLower() != "face" && choice.ToString().ToLower() != "pile")
            {
                await ctx.RespondAsync(utilsService.CreateNewEmbed("Error", DiscordColor.Red, "Impossible choice"));
                return;

            }             
              

                   
            var random = new Random();
            var rnd_number = random.Next(0,2);

            string[] name = { "pile", "face" };

            var random_name = name[rnd_number];

          


            if (choice.ToString().ToLower() == random_name)
            {
                await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                return;

            }
            else
                await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $"You lose because it's {random_name}"));
            



        }

   

    }
}

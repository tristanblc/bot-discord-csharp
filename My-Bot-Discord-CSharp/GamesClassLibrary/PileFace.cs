using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesClassLibrary
{
    public sealed class PileFace
    {
        private readonly IUtilsService utilsService = new UtilsService();
        private CommandContext ctx { get; init; }
        private string choice { get; init; }


        public PileFace(CommandContext ctx_, string choice_)
        {
            ctx = ctx_;
            choice = choice_;    
        }


        public async void PlayGame()
        {

            if (choice.ToString().ToLower() != "face" && choice.ToString().ToLower() != "pile")
            {
                await ctx.RespondAsync(utilsService.CreateNewEmbed("Error", DiscordColor.Red, "Impossible choice"));
                return;

            }



            var random = new Random();
            var rnd_number = random.Next(0, 2);

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

using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesClassLibrary
{
    public  class RockPaperScissors
    {
        private readonly IUtilsService utilsService = new UtilsService();
        private CommandContext ctx { get; init; }

        private DiscordEmoji[] EmojiCache;

        public RockPaperScissors(CommandContext Ctx)
        {
            ctx = Ctx;

        }

        public async void PlayRockScissor()
        {
            var question = $"What is your choice?";

            if (!string.IsNullOrEmpty(question))
            {

                var random = new Random();
                var rnd_number = random.Next(0, 3);

                string[] name = { "rock", "leaf","scissors" };

                var random_name = name[rnd_number];


                var choice = "";

                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client,":rock:"),
                        DiscordEmoji.FromName(client, ":fallen_leaf:"),
                        DiscordEmoji.FromName(client,":scissors:"),
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 0, 15));
                var rockvote = pollResult[0].Total;
                var leafvote = pollResult[1].Total;
                var scissorsvote = pollResult[2].Total;


                
                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("**");
                if (rockvote < leafvote && scissorsvote < leafvote)
                    choice = "leaf";
                if (leafvote <rockvote && scissorsvote < rockvote)
                    choice = "rock";
                if (rockvote < scissorsvote && leafvote < scissorsvote)
                    choice = "scissors";

                await ctx.RespondAsync($"you choose {choice}");
                switch (choice)
                    {
                        case "rock":
                            if(random_name == "scissors")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                            if(random_name == "rock")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("Tie", DiscordColor.Green, $"Tie"));
                            else
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $"AI chose leaf"));
                            break;
                        case "leaf":
                            if (random_name == "rock")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                            if (random_name == "leaf")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("Tie", DiscordColor.Green, $"Tie"));
                            else
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $" AI chose scissors"));
                            break;
                            break;
                        case "scissors":
                            if (random_name == "rock")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                            if (random_name == "scissors")
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("Tie", DiscordColor.Green, $"Tie"));
                            else
                                await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $" AI chose rock"));
                            break;
                    }

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }

        }
    }
}

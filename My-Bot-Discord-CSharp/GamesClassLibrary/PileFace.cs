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
    public sealed class PileFace
    {
        private readonly IUtilsService utilsService = new UtilsService();
        private CommandContext ctx { get; init; }

        private DiscordEmoji[] EmojiCache;

        public PileFace(CommandContext Ctx)
        {
            ctx = Ctx;
        }


        public async void PlayGame()
        {


            var question = $"What is your choice? Pile ou face";

            if (!string.IsNullOrEmpty(question))
            {

                var random = new Random();
                var rnd_number = random.Next(0, 2);

                string[] name = { "pile", "face" };

                var random_name = name[rnd_number];


                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                    EmojiCache = new[] {
                        DiscordEmoji.FromName(client,":point_left:"),
                        DiscordEmoji.FromName(client, ":point_right:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append(question);
                pollStartText.Append($"\nPile :{DiscordEmoji.FromName(client, ":point_left:")}");
                pollStartText.Append($"\nFace :{DiscordEmoji.FromName(client, ":point_right:")}");
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0,0,45));
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {
                    pollResultText.Append("You choose face");
                    if (random_name == "face")
                    {
                        await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                        return;

                    }
                    else
                    {
                        await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $"You lose because it's {random_name}"));
                    }


                }
                else if (yesVotes == noVotes)
                {
                    pollResultText.Append("Undecided");
                    return;
                }
                else
                {
                    pollResultText.Append("You choose pile");
                    if (random_name == "pile")
                    {
                        await ctx.RespondAsync(utilsService.CreateNewEmbed("You win", DiscordColor.Green, $"GG"));
                        return;

                    }
                    else
                    {
                        await ctx.RespondAsync(utilsService.CreateNewEmbed("You lose", DiscordColor.Red, $"You lose because it's {random_name}"));
                    }
                }
            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }



        }


    }
}

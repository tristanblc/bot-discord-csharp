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
    public class JustPrice
    {
        private readonly IUtilsService utilsService = new UtilsService();
        private CommandContext ctx { get; init; }

        private DiscordEmoji[] EmojiCache;

        public JustPrice(CommandContext Ctx)
        {
            ctx = Ctx;
        }
        public async void PlayGame()
        {
            int iterator = 15;
            var question = $"Find product price ?";

            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interact = client.GetInteractivity();

                var random = new Random();
                var price = random.Next(0, 100000);
       
                var embed = utilsService.CreateNewEmbed(question, DiscordColor.Blue, "?");
                var sendEmbed = await ctx.RespondAsync(embed.Build());
                int i = 0;
                while(i < iterator)
                {
                    var message = interact.WaitForMessageAsync(
                        x => x.Channel.Id == ctx.Channel.Id
                        && x.Author.Id == ctx.Member.Id
                        && sendEmbed.Interaction.User.IsCurrent, new TimeSpan(0, 0, 15));
                    var answer = 0;
                    if( price < answer)
                        await ctx.RespondAsync($"\n The price is higher than your answer  {DiscordEmoji.FromName(client, ":point_top:")}");
                    if (price  >  answer)
                        await ctx.RespondAsync($"\n The price is lower than your answer   {DiscordEmoji.FromName(client, ":point_lower:")}");
                    if (price ==  answer)
                        await ctx.RespondAsync($"\n You win {DiscordEmoji.FromName(client,":thumbsup:")}");   
                    else
                        await ctx.RespondAsync($"\n You cannot find it. It was {price} {DiscordEmoji.FromName(client, ":thumbsdown:")}");
                }

                i++;

            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }

        }
    }
}

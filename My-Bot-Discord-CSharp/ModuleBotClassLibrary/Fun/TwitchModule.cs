using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Fun
{
    public class TwitchModule : BaseCommandModule
    {
        private TwitchService TwitchService { get; init; }

        private IUtilsService UtilsService { get; init; }

        public TwitchModule()
        {
            var builder = new ConfigurationBuilder()
                          .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                          .AddEnvironmentVariables();

            IConfiguration AppSetting = builder.Build();
        
           TwitchService = new TwitchService(AppSetting["Twitch:accesstoken"],AppSetting["Twitch:clientId"]);

        }

        [Command("gettwitchUser")]
        public async Task HandleGetTwitchUser(CommandContext ctx,string userId)
        {
            try
            {
                var user = TwitchService.GetUserById(userId);
                var embed = TwitchService.ConvertTwitchUserToEmbed(user);

                await ctx.RespondAsync(embed.Build());

            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getFollowingUser")]
        public async Task HandleGetFollowingUser(CommandContext ctx, string userId)
        {
            try
            {

                var followers = TwitchService.GetFollowedUser(userId);

                followers.ToList().ForEach(follower =>
                {
                    var embed = TwitchService.ConvertFollowersTwitchToEmbed(follower);
                    ctx.RespondAsync(embed.Build());

                });

            }
            catch(Exception ex)
            {

                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }


        [Command("getStreams")]
        public async Task HandleGetStreams(CommandContext ctx,string userId)
        {
            try
            {
             
                var stream = TwitchService.GetStreamById(userId);
                var embed = TwitchService.ConvertStreamTwitchToEmbed(stream);

                await ctx.RespondAsync(embed.Build());

            }
            catch(Exception ex) {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }


    }
}

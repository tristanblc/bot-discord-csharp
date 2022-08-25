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
        public async Task HandleGetTwitchUser(CommandContext ctx,string username)
        {
            try
            {

                var user = TwitchService.GetUserByName(username);
                var embed = TwitchService.ConvertTwitchUserToEmbed(user);

                await ctx.RespondAsync(embed.Build());

            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getFollowuser")]
        public async Task HandleGetFollowingUser(CommandContext ctx, string name)
        {
            try
            {
                var user = TwitchService.GetUserByName(name);

                var followers = TwitchService.GetFollowedUser(user.Id);

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
        public async Task HandleGetStreams(CommandContext ctx,string name)
        {
            try
            {

                var user = TwitchService.GetUserByName(name);
                var stream = TwitchService.GetStreamById(user.Id);
                var embed = TwitchService.ConvertStreamTwitchToEmbed(stream);

                await ctx.RespondAsync(embed.Build());

            }
            catch(Exception ex) {
                var exception = UtilsService.CreateNewEmbed("Pas en live", DiscordColor.White, $"Pas de stream pour {name}");

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getClip")]
        public async Task HandleGetClip(CommandContext ctx, string username)
        {
            try
            {

                var user = TwitchService.GetUserByName(username);
                var clip = TwitchService.GetLatestClipByUsername(username);
                var extract = TwitchService.DownloadClipFromTwitch(clip);
                var message = TwitchService.ConvertClipToMessage(extract);
                await message.SendAsync(ctx.Channel);

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("Pas en live", DiscordColor.White, $"Pas de stream from{ username}");

                await ctx.RespondAsync(exception.Build());
            }
        }
        [Command("getlatestclip")]
        public async Task HandleGetLatestClip(CommandContext ctx, string username)
        {
            try
            {

                var user = TwitchService.GetUserByName(username);
                var clips = TwitchService.Get10LatestClipsFromUser(username);
                clips.ForEach(clip =>
                {
                    var embed = TwitchService.ConvertTwitchClipToEmbed(clip);
                    ctx.RespondAsync(embed.Build());
                }
                );
         

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("Pas en live", DiscordColor.White, $"Pas de stream from{username}");

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("twitchemoji")]
        public async Task HandleGetUserEmoji(CommandContext ctx, string username)
        {
            try
            {
                var user = TwitchService.GetUserByName(username);
                TwitchService.ConvertEmojiToEmbed(user.Id, ctx);
               
                

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("pas de jeu", DiscordColor.Azure, "");

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("game")]
        public async Task HandleGetGames(CommandContext ctx,string game)
        {
            try
            {
                var games = TwitchService.GetGameFromName(game);
                var embed = TwitchService.ConvertGameToEmbed(games);
                await ctx.RespondAsync(embed.Build());

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("pas de jeu", DiscordColor.Azure, "");

                await ctx.RespondAsync(exception.Build());
            }
        }

    }
}

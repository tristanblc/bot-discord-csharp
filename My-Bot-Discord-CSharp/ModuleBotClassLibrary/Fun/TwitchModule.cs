using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using ModuleBotClassLibrary.RessourceManager;
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
            UtilsService = new UtilsService();

        }

        [Command("gettwitchUser")]
        [DescriptionCustomAttribute("twitchUserInfoCmd")]
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
        [DescriptionCustomAttribute("twitchFollowingCommand")]
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
        [DescriptionCustomAttribute("twstreaminfCmd")]
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
        [DescriptionCustomAttribute("latestClipCmd")]
        public async Task HandleGetClip(CommandContext ctx, string username)
        {
            try
            {

                var user = TwitchService.GetUserByName(username);
                var clip = TwitchService.GetLatestClipByUsername(username);
                var embed = TwitchService.ConvertTwitchClipToEmbed(clip);
                ctx.RespondAsync(embed.Build());

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("Pas en live", DiscordColor.White, $"Pas de stream from{ username}");

                await ctx.RespondAsync(exception.Build());
            }
        }
        [Command("getlatestclip")]
        [DescriptionCustomAttribute("twEmojiCmd")]
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
        [DescriptionCustomAttribute("twEmojiCmd")]
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
        [DescriptionCustomAttribute("gameTwitchCmd")]
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

        [Command("downloadlateststream")]
        [DescriptionCustomAttribute("downloadlateststreamCmd")]
        public async Task HandleGetLatestStream(CommandContext ctx, string broadcastername)
        {
            try
            {
                TwitchService.DownloadLatestVideo(broadcastername);
                var result = UtilsService.CreateNewEmbed("Steam downladed", DiscordColor.Azure, "");

                await ctx.RespondAsync(result.Build());

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("pas de jeu", DiscordColor.Azure, "");

                await ctx.RespondAsync(exception.Build());
            }
        }

    }
}

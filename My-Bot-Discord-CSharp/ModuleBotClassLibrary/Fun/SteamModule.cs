using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
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
    public class SteamModule : BaseCommandModule
    {
        private SteamService SteamService { get; init; }

        private IUtilsService UtilsService { get; init; }

        private DiscordEmoji[] EmojiCache;
        public SteamModule()
        {
            var builder = new ConfigurationBuilder()
                                           .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                           .AddEnvironmentVariables();

            IConfiguration AppSetting = builder.Build();

            SteamService = new SteamService(AppSetting["Steam:apikey"]);
            UtilsService = new UtilsService();
        }



        [Command("userSteam")]
        [DescriptionCustomAttribute("steamUserInfoCommand")]
        public async Task HandleGetPostsFromSub(CommandContext ctx, ulong steamUserId)
        {
            try
            {
                var users = SteamService.GetListsSteamUser(steamUserId);
                users.ToList().ForEach(user =>
                {
                    var builder = SteamService.ConvertSteamUsetToEmbed(user);
                    ctx.RespondAsync(builder.Build());
                }
                );

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());

            }
        }

        [Command("friendlist")]
        [DescriptionCustomAttribute("steamUserFriendListCommand")]
        public async Task HandleGetFriendsList(CommandContext ctx, ulong steamUserId)
        {
            try
            {
                var friends = SteamService.GetFriendsListBySteamUserId(steamUserId);
                friends.ForEach(friend =>
                {
                    var embed = SteamService.ConvertFriendModelToEmbed(friend);
                    ctx.RespondAsync(embed.Build());
                }
                );


            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getapp")]
        [DescriptionCustomAttribute("steamAppInfoCommand")]
        public async Task HandleGetAppFromSteam(CommandContext ctx, string appname)
        {
            try
            {
                var app = SteamService.GetSteamAppByName(appname);
                app.ForEach(ap =>
                {
                    var embed = SteamService.ConvertSteamAppToEmbed(ap);
                    ctx.RespondAsync(embed.Build());
                });

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }
        [Command("getapps")]
        [DescriptionCustomAttribute("steamAppInfoCommand")]
        public async Task HandleGetApps(CommandContext ctx)
        {
            try
            {
                var apps = SteamService.GetSteamApps();
                apps.ToList().ForEach(app =>
                {
                    var embed = SteamService.ConvertSteamAppToEmbed(app);
                    ctx.RespondAsync(embed.Build());
                });

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }


        [Command("getappsname")]
        [DescriptionCustomAttribute("steamAppbyNameCommand")]
        public async Task HandleGetAppByName(CommandContext ctx, string appname)
        {
            try
            {
                var apps = SteamService.GetSteamAppByName(appname);
                apps.ToList().ForEach(app =>
                {
                    var embed = SteamService.ConvertSteamAppToEmbed(app);
                    ctx.RespondAsync(embed.Build());
                });

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getappsbydateasc")]
        [DescriptionCustomAttribute("steamAppsAscCommand")]

        public async Task HandleGetAppsByDateAsc(CommandContext ctx, DateTime date)
        {
            try
            {
                var apps = SteamService.GetSteamAppsByDateAsc(date.ToString());
                apps.ToList().ForEach(app =>
                {
                    var embed = SteamService.ConvertSteamAppToEmbed(app);
                    ctx.RespondAsync(embed.Build());
                });

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getappsbydatedesc")]
        [DescriptionCustomAttribute("steamAppsDescCommand")]
        public async Task HandleGetAppsByDateDesc(CommandContext ctx, DateTime date)
        {
            try
            {
                var apps = SteamService.GetSteamAppsByDateDesc(date.ToString());
                apps.ToList().ForEach(app =>
                {
                    var embed = SteamService.ConvertSteamAppToEmbed(app);
                    ctx.RespondAsync(embed.Build());
                });

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getnewsforapp")]
        [DescriptionCustomAttribute("steamAppCommand")]
        public async Task HandleGetNewsForApp(CommandContext ctx, uint appId)
        {
            try
            {

                var news = SteamService.GetSteamNewsForApp(appId);
                news.NewsItems.ToList().ForEach(lastNews =>
                {
                    ctx.RespondAsync("```" + lastNews.Contents.ToLower() + "```");
                });



            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("pollGame")]
        public async Task HandlePollGame(CommandContext ctx, string gamename)
        {
            try
            {
                var app = SteamService.GetSteamAppByName(gamename);
                var appinfo = SteamService.GetSteamAppsById(app.First().AppId);
                var question = $"Do you like {gamename}";
                var builder = new DiscordEmbedBuilder()
                {
                    Title = $"Community like {app.First().Name}",
                    Color = DiscordColor.Blue,
                };
             
                
                if (!string.IsNullOrEmpty(question))
                {
                    var client = ctx.Client;
                    var interactivity = client.GetInteractivity();
                    if (EmojiCache == null)
                    {
                        EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                    }

                    // Creating the poll message
                    var pollStartText = new StringBuilder();
                    pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                    pollStartText.Append(question);
                    var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                    // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                    var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis, new TimeSpan(0, 5, 0, 0));
                    var yesVotes = pollResult[0].Total;
                    var noVotes = pollResult[1].Total;

                    // Printing out the result
                    var pollResultText = new StringBuilder();
                    pollResultText.AppendLine(question);
                    pollResultText.Append("Poll result: ");
                    pollResultText.Append("**");
                    if (yesVotes > noVotes)
                    {
                        await ctx.RespondAsync(builder.Build());                 

                    }
                    else if (yesVotes == noVotes)
                    {
                        throw new Exception("No choice ");
                    }
                    if (yesVotes < noVotes)
                    {

                        builder.Title = $"Community doesn't like {gamename}";

                        builder.Color = DiscordColor.Red;

                        await ctx.RespondAsync(builder.Build());
                    }

                }
                else
                {
                    await ctx.RespondAsync("Error: the question can't be empty");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}

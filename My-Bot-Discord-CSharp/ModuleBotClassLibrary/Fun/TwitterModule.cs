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
     public  class TwitterModule : BaseCommandModule
    {
        private  TwitterService TwitterClient { get; init; }
        private IUtilsService UtilsService { get; init; }
        private ILoggerProject Logger { get; init; }
        public TwitterModule()
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                 .AddEnvironmentVariables();

            IConfiguration AppSetting = builder.Build();

            TwitterClient = new TwitterService(AppSetting["Twitter:apiKey"], AppSetting["Twitter:apiToken"], AppSetting["Twitter:accessToken"], AppSetting["Twitter:accessSecret"]);
            UtilsService = new UtilsService();
            Logger = new LoggerProject();
        }


        [Command("getTwitterUser")]
        [DescriptionCustomAttribute("twitterUser")]
        public async Task HandleGetTwitterUser(CommandContext ctx, string username)
        {
            try
            {
                var user = TwitterClient.GetUsers(username);

                var embed = TwitterClient.ConvertUserToEmbed(user);
                await ctx.RespondAsync(embed.Build());

            }
            catch (Exception ex)
            { 
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());
                var message = "error";
                Logger.WriteLogErrorLog(message);
                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("gettweets")]
        public async Task HandleGetTweets(CommandContext ctx, string search)
        {
            try
            {
                var tweets = TwitterClient.GetTweetsByName(search);
                int i = 0;
                tweets.ForEach(tweet =>
                {
                    var embed = TwitterClient.ConvertTweetToEmbed(tweet);
                    ctx.RespondAsync(embed.Build());
                    if (i == 20)
                        return;
                    i++;
                });
     

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());
                var message = "error";
                Logger.WriteLogErrorLog(message);
                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("gettweetsuser")]
        public async Task HandleGetTweetsUser(CommandContext ctx, string search, string limit)
        {
            try
            {
                var tweets = TwitterClient.GetTweetsByUser(search, limit);
                int i = 0;
                tweets.ForEach(tweet =>
                {
                    var embed = TwitterClient.ConvertTweetToEmbed(tweet);
                    ctx.RespondAsync(embed.Build());
                    if (i == 20)
                        return;
                    i++;
                });


            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());
                var message = "error";
                Logger.WriteLogErrorLog(message);
                await ctx.RespondAsync(exception.Build());
            }
        }

    }
}

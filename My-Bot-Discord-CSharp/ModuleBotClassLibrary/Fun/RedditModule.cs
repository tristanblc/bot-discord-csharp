using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Fun
{
    public class RedditModule : BaseCommandModule
    {
        private RedditService RedditService { get; init; }
        private UtilsService UtilsService { get; init; }
        public RedditModule()
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration AppSetting = builder.Build();

            RedditService = new RedditService(AppSetting["Reddit:appId"], AppSetting["Reddit:appSecret"], AppSetting["Reddit:browse"]);
            UtilsService = new UtilsService();
        }


        [Command("getpostsfromsub")]
        [Description("get reddit posts by description")]
        public async Task HandleGetPostsFromSub(CommandContext ctx, string subname)
        {
            try
            {
                var posts = RedditService.GetPostsFromSubRedditName(subname);
                posts.ToList().ForEach(post =>
                {
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
                    ctx.RespondAsync(builder.Build());
                }
                );

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.ToString());

            }
        }

        [Command("getlatestpost")]
        [Description("get lastest reddit posts by subname")]
        public async Task HandleGetLatestPost(CommandContext ctx, string subname)
        {
            try
            {
                var post = RedditService.GetLatestPostFromSubReddit(subname);
                var builder = RedditService.ConvertPostToDiscordEmbed(post);
                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());

            }
        }
    

        [Command("getbest")]
        [Description("get best reddit posts by subname")]
        public async Task HandleGetBestPostFromSub(CommandContext ctx, string subname)
        {
            try
            {
                var posts = RedditService.GetBestPostSubReddit(subname);

                posts.ToList().ForEach( post =>
                {
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
                    ctx.RespondAsync(builder.Build());

                });

                
            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());


            }

        }

        [Command("gettopdaily")]
        [Description("get top daily reddit posts by subname")]
        public async Task HandleGetTopDaily(CommandContext ctx, string subname)
        {
            try
            {

                var posts = RedditService.GetTopDailyPost(subname);
                posts.ToList().ForEach(post =>
                {
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
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

        [Command("gethotsreddit")]
        [Description("get hot reddit posts by subname")]
        public async Task HandleGetHotsReddit(CommandContext ctx, string subname)
        {
            try
            {
                var posts = RedditService.GetHotPostFromSub(subname);
                posts.ToList().ForEach(post =>
                {
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
                    ctx.RespondAsync(builder.Build());

                });
            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }


        [Command("changeNSFW")]
        public async Task HandleUserRedditPreference(CommandContext ctx, string setterbool)
        {
            try
            {
                bool isSet = false;
                switch (setterbool.ToLower())
                {
                    case "false":
                        isSet = false;
                        break;
                      
                    case "true":
                        isSet = true;
                        break;
                   default:
                        var breakerMessage = UtilsService.CreateNewEmbed($"Impossible choice", DiscordColor.Red, $"You have to choice false or true");
                        await ctx.RespondAsync(breakerMessage.Build());

                        break;


                }
                RedditService.UpdateBotPreferenceNSFW(isSet);
                var clientMessage = UtilsService.CreateNewEmbed($"Reddit policy changed",DiscordColor.Green, $"NFSW is set to {isSet.ToString()}");
                await ctx.RespondAsync(clientMessage.Build());
            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getcomments")]
        [Description("get comment on reddit posts by subname & title")]
        public async Task HandleGetCommentsFromPost(CommandContext ctx,string subname,string title)
        {
            try
            {

                var post = RedditService.GetPostFromSub(title, subname);
                var comments = RedditService.GetCommentsFromPost(post);
                comments.ToList().ForEach(comment =>
                {
                    var builder = RedditService.ConvertCommmentToDiscordEmbed(comment);
                    ctx.RespondAsync(builder.Build());
                    
                });
            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getcountreplies")]
        [Description("get count replies")]
        public async Task HandleGetCountReplies(CommandContext ctx, string subname,string title)
        {
            try
            {
                var post = RedditService.GetPostFromSub(title, subname);
                var embed = RedditService.GetCountReplies(post);
                await ctx.RespondAsync(embed.Build());
            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getuserdata")]
        [Description("get user info")]
        public async Task HandleGetUserInfo(CommandContext ctx, string username)
        {
            try
            {
                var user = RedditService.GetUser(username);
                var embed = RedditService.ConvertUserInfoToEmbed(user);
                await ctx.RespondAsync(embed.Build());
            }
            catch (Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());

            }
        }

        [Command("getuserposts")]
        [Description("get user post")]
        public async Task HandleGetUsersInfo(CommandContext ctx, string username)
        {
            try
            {
                var user = RedditService.GetUser(username);
                var posts = RedditService.GetUserPosts(user);

                posts.ToList().ForEach(post =>
                {
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
                    ctx.RespondAsync(builder.Build());
                });

            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

    }


    

}


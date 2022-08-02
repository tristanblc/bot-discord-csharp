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

            RedditService = new RedditService(AppSetting["Reddit:appId"], AppSetting["Reddit:token"]);
            UtilsService = new UtilsService();
        }


        [Command("getpostsfromsub")]
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
        public async Task HandleGetLatestPost(CommandContext ctx, string subname)
        {
            try
                {
                    var post  = RedditService.GetLatestPostFromSubReddit(subname);
                    var builder = RedditService.ConvertPostToDiscordEmbed(post);
                    await ctx.RespondAsync(builder.Build());

            }
                catch (Exception ex)
                {
                    var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());

                }


            }


        }
    }


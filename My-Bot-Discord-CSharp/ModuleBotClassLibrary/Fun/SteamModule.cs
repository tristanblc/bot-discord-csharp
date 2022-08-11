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
    public  class SteamModule : BaseCommandModule
    {
        private SteamService SteamService { get; init; }
        private IUtilsService UtilsService { get; init; }


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

                await ctx.RespondAsync(exception.ToString());

            }
        }

        [Command("friendlist")]
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

                await ctx.RespondAsync(exception.ToString());
            }
        }
        [Command("checkVAC")]
        public async Task HandleCheckVAC(CommandContext ctx,ulong steamUserId)
        {
            try
            {
                var user = SteamService.GetBansSteamUser(steamUserId).First();
                var embed = SteamService.ConvertPlayerBans(user);
                ctx.RespondAsync(embed.Build());

            }
            catch(Exception ex)
            {
                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.ToString());
            }
        }
               
    }
}

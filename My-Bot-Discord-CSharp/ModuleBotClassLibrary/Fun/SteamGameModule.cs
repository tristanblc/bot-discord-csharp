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
    public class SteamGameModule : BaseCommandModule
    {
        private SteamGameService SteamGameService { get; init; }
        private IUtilsService UtilsService { get; init; }

        public SteamGameModule()
        {
            var builder = new ConfigurationBuilder()
                                                     .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                                     .AddEnvironmentVariables();

            IConfiguration AppSetting = builder.Build();
            SteamGameService = new SteamGameService(AppSetting["Steam:apikey"]);
            UtilsService = new UtilsService();
        }


        [Command("csgoserver")]
        [DescriptionCustomAttribute("csgoCommand")]
        public async Task HandleGetCsgoServerStatus(CommandContext ctx)
        {
            try
            {
                var server = SteamGameService.GetServerStatus();
                var embed = SteamGameService.ConvertServerStatusToEmbed(server);
                await ctx.RespondAsync(embed.Build());

            }
            catch (Exception ex)
            {

                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getasset")]
        [DescriptionCustomAttribute("assetCommand")]
        public async Task HandleGetAssetInfo(CommandContext ctx, string appname)
        {
            try
            {
                var app = SteamGameService.GetAssetInfo(appname);
                var embed = SteamGameService.ConvertAssetClassToEmbed(app);
                await ctx.RespondAsync(embed.Build());

            }
            catch (Exception ex)
            {

                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }

        [Command("getGoldenWrench")]

        [DescriptionCustomAttribute("tfCommand")]
        public async Task HandleGetGoldenWrench(CommandContext ctx, int number)
        {
            try
            {

                var i = 0;
                var wrenchs = SteamGameService.GetGoldenWrenchModels().ToList();
                while (i < number)
                {
                    var embed = SteamGameService.ConvertGoldenWrenchToEmbed(wrenchs[i]);
                    await ctx.RespondAsync(embed.Build());
                    i++;
                }

            }
            catch (Exception ex)
            {

                var exception = UtilsService.CreateNewEmbed("error", DiscordColor.White, ex.ToString());

                await ctx.RespondAsync(exception.Build());
            }
        }


    }


}

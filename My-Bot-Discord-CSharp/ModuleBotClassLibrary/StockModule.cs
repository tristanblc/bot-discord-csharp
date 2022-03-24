using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{

    public class StockModule : BaseCommandModule
    {
        [Command("entreprise")]
        public async Task EntrepriseCommand(CommandContext ctx, string entreprisename)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }

        [Command("market")]
        public async Task MarketInfoCommand(CommandContext ctx, string market)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }

        [Command("marketplace")]
        public async Task MarketPlaceCommand(CommandContext ctx, string entreprisename)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }


        [Command("10values")]
        public async Task LastMaxValueCommand(CommandContext ctx, string entreprisename)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }

        [Command("apiInfo")]
        public async Task ApiInfoCommand(CommandContext ctx, string entreprisename)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }











    }
}

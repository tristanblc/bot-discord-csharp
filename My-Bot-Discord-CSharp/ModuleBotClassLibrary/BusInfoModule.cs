using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    internal class BusInfoModule : BaseCommandModule
    {

        [Command("arrets")]
        public async Task arretsCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }

        [Command("arret")]
        public async Task PingCommand(CommandContext ctx, string name)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }
    }
}

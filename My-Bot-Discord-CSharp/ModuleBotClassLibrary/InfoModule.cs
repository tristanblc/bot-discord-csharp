using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ModuleBotClassLibrary
{
    public class InfoModule : BaseCommandModule
    {
        [Command("ping")]
        public async Task PingCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Ping! Pong!");
        }

      



    }

}
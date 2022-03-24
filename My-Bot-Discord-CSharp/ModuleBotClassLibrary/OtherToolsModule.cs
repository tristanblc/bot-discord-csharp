using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class OtherToolsModule : BaseCommandModule
    {
        [Command("random")]
        public async Task RandomCommand(CommandContext ctx, int min, int max)
        {
            var random = new Random();
            await ctx.RespondAsync($"Le chiffre est : {random.Next(min, max)}");

        }


        [Command("avatar")]
        public async Task AvatarCommand(CommandContext ctx, DiscordMember member)
        {

            var url = member.GetAvatarUrl(DSharpPlus.ImageFormat.Auto);
            await ctx.RespondAsync(url);
        }

   



    }
}

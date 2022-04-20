using DSharpPlus;
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
    public class AdminModule : BaseCommandModule
    {
        [Command("ban")]
        [Description("Ban user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task ban(CommandContext ctx, DiscordMember member, string reason)
        {
            await member.BanAsync(1, reason);
        }


        [Command("unban")]
        [Description("unban user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task unban(CommandContext ctx, DiscordMember member, string reason)
        {
            await member.UnbanAsync(reason);

        }


        [Command("purge")]
        [Description("purge channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task Purge(CommandContext ctx)
        {

            IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAsync();
            

            await ctx.Channel.DeleteMessagesAsync(deletesMessagesLists);
            await ctx.RespondAsync("End clean channel " + ctx.User.Mention);

        }

        [Command("clean")]
        [Description("clear x message in channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task clean(CommandContext ctx, string reason)
        {

            ulong number = 0;

            ulong.TryParse(reason, out number);

            IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAfterAsync(number);


            await ctx.Channel.DeleteMessagesAsync(deletesMessagesLists);
            await ctx.RespondAsync("End clean + " + number+"  message(s) in channel " + ctx.User.Mention);



        }


        [Command("deaf")]
        [Description("deaf user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task deafUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsDeafened)
            {
                await ctx.RespondAsync("Member " + member.Username.ToString() + " is Deafened");

                return;
            }

            await member.SetDeafAsync(true, reason);

            await ctx.RespondAsync("Member " + member.Username.ToString() + " is deafened now");



        }


        [Command("undeaf")]
        [Description("undeaf user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task undeafUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsDeafened == false)
            {
                await ctx.RespondAsync("Member " + member.Username.ToString() + " is not Deafened");

                return;
            }

            await member.SetDeafAsync(false, reason);

            await ctx.RespondAsync("Member " + member.Username.ToString() + " is not deafened now");



        }



        [Command("mute")]
        [Description("mute user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task muteUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsMuted == true)
            {
                await ctx.RespondAsync("Member " + member.Username.ToString() + " is mute");

                return;
            }

            await member.SetMuteAsync(true, reason);

            await ctx.RespondAsync("Member " + member.Username.ToString() + " is mute now");



        }


        [Command("unmute")]
        [Description("unmute user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task UnMuteUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsMuted == false)
            {
                await ctx.RespondAsync("Member " + member.Username.ToString() + " is not muted");

                return;
            }

            await member.SetMuteAsync(false, reason);

            await ctx.RespondAsync("Member" + member.Username.ToString() + " is not muted now");



        }



        [Command("addrole")]
        [Description("add role to  user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task addRoleUser(CommandContext ctx, DiscordMember member,DiscordRole role)
        {
            if (member.Roles.Contains(role))
            {
                await ctx.RespondAsync( member.Username.ToString() + " have the role "+ role.Name );

                return;
            }

           await member.GrantRoleAsync(role);  

            await ctx.RespondAsync("Add role : " + role.Name +" to "+member.Username.ToString());



        }

        [Command("removerole")]
        [Description("remove role to  user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task removeRoleUser(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            if (member.Roles.Contains(role))
            {

                await member.RevokeRoleAsync(role);

                await ctx.RespondAsync("Remove role : " + role.Name + " to " + member.Username.ToString());
            }
        }


        [Command("timeout")]
        [Description("timeout user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task timeoutUser(CommandContext ctx, DiscordMember member, string time)
        {
            DateTime.TryParse(time, out var now);

            if (now == null)
            {
                await ctx.RespondAsync("Null time");
                return;
            }

          

            await ctx.RespondAsync("User " + member.Username + " timeout for "+ time.ToString());

        }




        [Command("invite")]
        [Description("invite")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task inviteUser(CommandContext ctx , DiscordChannel channel,int time)
        {
            var invite  = await channel.CreateInviteAsync(time);


            await ctx.RespondAsync(invite.ToString());

        }
        






    }
}

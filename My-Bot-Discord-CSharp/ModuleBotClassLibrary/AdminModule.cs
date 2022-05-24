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
            var builder = new DiscordEmbedBuilder
            {
                Title = "Ban",

                Color = DiscordColor.Green,

                Description = $"{member.DisplayName} ban !"

            };
            await member.BanAsync(1, reason);
            await ctx.RespondAsync(builder.Build());
        }


        [Command("unban")]
        [Description("unban user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task unban(CommandContext ctx, DiscordMember member, string reason)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = "Unban",

                Color = DiscordColor.Green,

                Description = $"{member.DisplayName} Unban !"

            };
            await member.UnbanAsync(reason);
            await ctx.RespondAsync(builder.Build());
        }


    
        [Command("clean-all")]
        [Description("clear all messages in channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task CleanAChannel(CommandContext ctx)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = $"Clear discord channel {ctx.Channel.Name}",

                Color = DiscordColor.Green,



            };
            try
            {
                
                IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAsync();

                foreach(DiscordMessage message in deletesMessagesLists)
                {
                    await ctx.Channel.DeleteMessageAsync(message);
                }
             
            
                builder.Description = "End clean all  messages in channel";
                await ctx.RespondAsync(builder.Build());
            }
            catch(Exception ex)
            {
                
                builder.Description = "Error";
                builder.Color = DiscordColor.Red;
                await ctx.RespondAsync(builder.Build());
            }




        }

        [Command("clean")]
        [Description("clear x message in channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task clean(CommandContext ctx, string reason)
        {

           
            ulong number = 0;

            ulong.TryParse(reason, out number);
            var builder = new DiscordEmbedBuilder
            {
                Title = $"Clear discord channel {ctx.Channel.Name} - {number} message(s)",

                Color = DiscordColor.Green,



            };

            IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAfterAsync(number);
            foreach (DiscordMessage message in deletesMessagesLists)
            {
                await ctx.Channel.DeleteMessageAsync(message);
            }


         
            builder.Description = "End clean + " + number + "  message(s) in channel " + ctx.User.Mention;
            await ctx.RespondAsync(builder.Build());



        }


        [Command("deaf")]
        [Description("deaf user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task deafUser(CommandContext ctx, DiscordMember member, string reason)
        {
           
            if (member.IsDeafened)
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"Deaf User { member.Username }",
                    Description= "Member " + member.Username.ToString() + " is already Deafened",

                    Color = DiscordColor.Green,



                };
                await ctx.RespondAsync(builder.Build());

                return;
            }

            var builder_= new DiscordEmbedBuilder
            {
                Title = $"Deaf User { member.Username }",
                Description = "Member " + member.Username.ToString() + " is  Deafened",

                Color = DiscordColor.Red,



            };
            await member.SetDeafAsync(true, reason);

            await ctx.RespondAsync(builder_.Build());



        }


        [Command("undeaf")]
        [Description("undeaf user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task undeafUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsDeafened == false)
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"Not Deaf User { member.Username }",
                    Description = "Member " + member.Username.ToString() + " is not Deafened",

                    Color = DiscordColor.Green,



                };
                await ctx.RespondAsync(builder.Build());

                return;
            }

            await member.SetDeafAsync(false, reason);

            var builder_ = new DiscordEmbedBuilder
            {
                Title = $"UnDeaf User { member.Username }",
                Description = "Member " + member.Username.ToString() + " is not deafened now",

                Color = DiscordColor.Red,



            };
            await ctx.RespondAsync(builder_.Build());
        



        }



        [Command("mute")]
        [Description("mute user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task muteUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsMuted == true)
            {

                var builder = new DiscordEmbedBuilder
                {
                    Title = $"Already Mute User { member.Username }",
                    Description = "Member " + member.Username.ToString() + " is already mute",

                    Color = DiscordColor.Green,



                };
                await ctx.RespondAsync(builder.Build());
            

                return;
            }

            await member.SetMuteAsync(true, reason);

            var builder_ = new DiscordEmbedBuilder
            {
                Title = $" Mute User { member.Username } is muted now",
                Description = "Member " + member.Username.ToString() + " is  muted",

                Color = DiscordColor.Green,



            };
            await ctx.RespondAsync(builder_.Build());
       



        }


        [Command("unmute")]
        [Description("unmute user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task UnMuteUser(CommandContext ctx, DiscordMember member, string reason)
        {
            if (member.IsMuted == false)
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Member is not muted now",
                    Description = $"Member" + member.Username.ToString() + " is not muted ",

                    Color = DiscordColor.Green,



                };
                await ctx.RespondAsync(builder.Build());
               

                return;
            }

            await member.SetMuteAsync(false, reason);


            var builder_ = new DiscordEmbedBuilder
            {
                Title = "Member is unmuted now",
                Description = $"Member" + member.Username.ToString() + " is  unmuted now",

                Color = DiscordColor.Green,



            };
            await ctx.RespondAsync(builder_.Build());

     



        }



        [Command("addrole")]
        [Description("add role to  user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task addRoleUser(CommandContext ctx, DiscordMember member,DiscordRole role)
        {
            if (member.Roles.Contains(role))
            {
                var builder_ = new DiscordEmbedBuilder
                {
                    Title = $"Have role {role.Name}",
                    Description = "Add role : " + role.Name + " to " + member.Username.ToString(),
                    Color = DiscordColor.Red


                };
                await ctx.RespondAsync(builder_.Build());

                return;
            }

           await member.GrantRoleAsync(role);
            var builder = new DiscordEmbedBuilder
            {
                Title = "Grant role",
                Description = "Add role : " + role.Name + " to " + member.Username.ToString(),
                Color = DiscordColor.Green


            };
            await member.RevokeRoleAsync(role);

            await ctx.RespondAsync(builder.Build());



        }

        [Command("removerole")]
        [Description("remove role to  user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task removeRoleUser(CommandContext ctx, DiscordMember member, DiscordRole role)
        {
            if (member.Roles.Contains(role))
            {


                var builder = new DiscordEmbedBuilder
                {
                    Title = "Role Remover",
                    Description = "Remove role : " + role.Name + " to " + member.Username.ToString(),
                    Color = DiscordColor.Green


                };
                await member.RevokeRoleAsync(role);
            

                await ctx.RespondAsync(builder.Build());
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
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Error ",
                    Description = "Time can not be null",
                    Color = DiscordColor.Red


                };
                await ctx.RespondAsync("Null time");
                return;
            }

            var builder_ = new DiscordEmbedBuilder
            {
                Title = "Timeout user",
                Description = "User { member.Username} is timeout for {time} seconds ",
                Color = DiscordColor.Green


            };
            await ctx.RespondAsync(builder_.Build());


        }




        [Command("invite")]
        [Description("invite")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task inviteUser(CommandContext ctx , DiscordChannel channel,int time)
        {

            var invite  = await channel.CreateInviteAsync(time);

            var builder_ = new DiscordEmbedBuilder
            {
                Title = "New invite",
                Description = invite.ToString(),
                Color = DiscordColor.Green,



            };
            await ctx.RespondAsync(builder_.Build());
      

        }




    

    }
}

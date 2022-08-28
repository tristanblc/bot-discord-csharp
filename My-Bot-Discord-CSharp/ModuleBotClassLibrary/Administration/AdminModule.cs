using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ModuleBotClassLibrary
{

    public class AdminModule : BaseCommandModule
    {
        private IUtilsService utilsService { get; init; }
        private IServerInfoService ServiceInfo { get; init; }

        public AdminModule()
        {
            utilsService = new UtilsService();
            PerformanceCounter performanceCounter = new PerformanceCounter();
            ServiceInfo = new ServerInfoService();
        }


        [Command("ban")]
        [Description("ban")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task ban(CommandContext ctx, DiscordMember member, string reason)
        {
            var builder = utilsService.CreateNewEmbed("Ban", DiscordColor.Green, $"{member.DisplayName} ban !");
            await member.BanAsync(1, reason);
            await ctx.RespondAsync(builder.Build());
        }





        [Command("changebotstatus")]
        [Description("Bot Status")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleBotStatus(CommandContext ctx, string reason, string? media)
        {

            var builder = utilsService.CreateNewEmbed("Set bot status", DiscordColor.Green, $"Set discord bot activity");

            try
            {
                DiscordActivity activity = null;
                switch (media.ToLower())
                {
                    case "play":
                        activity = new DiscordActivity(reason, ActivityType.ListeningTo);
                        break;
                    case "stream":
                        activity = new DiscordActivity(reason, ActivityType.Streaming);
                        break;

                    case "watch":
                        activity = new DiscordActivity(reason, ActivityType.Watching);
                        break;
                    default:
                        activity = new DiscordActivity(reason, ActivityType.Playing);
                        break;

                }

                await ctx.Client.UpdateStatusAsync(activity);

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(builder.Build());
            }

        }

        [Command("unban")]
        [Description("unban user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task unban(CommandContext ctx, DiscordMember member, string reason)
        {

            var builder = utilsService.CreateNewEmbed("Unban", DiscordColor.Green, $"{member.DisplayName} Unban !");
            await member.UnbanAsync(reason);
            await ctx.RespondAsync(builder.Build());
        }



        [Command("clean-all")]
        [Description("clear all messages in channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task CleanAChannel(CommandContext ctx)
        {

            var builder = utilsService.CreateNewEmbed($"Clear discord channel {ctx.Channel.Name}", DiscordColor.Green, "");
            try
            {

                IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAsync();

                foreach (DiscordMessage message in deletesMessagesLists)
                {
                    await ctx.Channel.DeleteMessageAsync(message);
                }


                builder.Description = "End clean all  messages in channel";
                await ctx.RespondAsync(builder.Build());
            }
            catch (Exception ex)
            {

                builder.Description = "Error";
                builder.Color = DiscordColor.Red;
                await ctx.RespondAsync(builder.Build());
            }




        }

        [Command("clear")]
        [Description("clear x message in channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task clean(CommandContext ctx, int number)
        {




            IEnumerable<DiscordMessage> deletesMessagesLists = await ctx.Channel.GetMessagesAsync(number);
            foreach (DiscordMessage message in deletesMessagesLists)
            {
                await ctx.Channel.DeleteMessageAsync(message);
            }

            var builder = utilsService.CreateNewEmbed($"Clear discord channel {ctx.Channel.Name} - {number} message(s)", DiscordColor.Green, "End clean   " + number + "message(s) delete" + ctx.User.Mention);
            await ctx.RespondAsync(builder.Build());



        }

        [Command("poll")]
        [Description("poll")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandlePoll(CommandContext ctx, string question)
        {
            var builder = utilsService.CreateNewEmbed($"Poll ", DiscordColor.Green, $"{question}");


            var emoji = DiscordEmoji.FromName(ctx.Client, ":thumbup:");
            var emoji2 = DiscordEmoji.FromName(ctx.Client, ":thumbdown:");

            var message = await ctx.RespondAsync(builder.Build());
            await message.CreateReactionAsync(emoji);
            await message.CreateReactionAsync(emoji2);


        }



        [Command("deaf")]
        [Description("deaf user")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task deafUser(CommandContext ctx, DiscordMember member, string reason)
        {

            if (member.IsDeafened)
            {
                var builder = utilsService.CreateNewEmbed($"Deaf User {member.Username}", DiscordColor.Green, "Member " + member.Username.ToString() + " is already Deafened");
                await ctx.RespondAsync(builder.Build());

                return;
            }
            var builder_ = utilsService.CreateNewEmbed($"Deaf User {member.Username}", DiscordColor.Red, "Member " + member.Username.ToString() + " is  Deafened");
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
                var builder = utilsService.CreateNewEmbed($"Not Deaf User {member.Username}", DiscordColor.Red, "Member " + member.Username.ToString() + " is not Deafened");

                await ctx.RespondAsync(builder.Build());

                return;
            }

            await member.SetDeafAsync(false, reason);

            var builder_ = utilsService.CreateNewEmbed($"Undeaf User {member.Username}", DiscordColor.Green, "Member " + member.Username.ToString() + " is not deafened now");
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
                    Title = $"Already Mute User {member.Username}",
                    Description = "Member " + member.Username.ToString() + " is already mute",

                    Color = DiscordColor.Green,



                };
                await ctx.RespondAsync(builder.Build());


                return;
            }

            await member.SetMuteAsync(true, reason);

            var builder_ = new DiscordEmbedBuilder
            {
                Title = $" Mute User {member.Username} is muted now",
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
        public async Task addRoleUser(CommandContext ctx, DiscordMember member, DiscordRole role)
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
                var builder = utilsService.CreateNewEmbed("Role Remover", DiscordColor.Azure, "Remove role : " + role.Name + " to " + member.Username.ToString());
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
            var builder = utilsService.CreateNewEmbed("Timeout user", DiscordColor.Azure, $"Null time");

            if (now == null)
            {

                await ctx.RespondAsync(builder.Build());
                return;
            }

            builder.Title = "Timeout user";
            builder.Description = "User { member.Username} is timeout for {time} seconds ";
            builder.Color = DiscordColor.Green;



            await ctx.RespondAsync(builder.Build());


        }




        [Command("invite")]
        [Description("invite")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task inviteUser(CommandContext ctx, DiscordChannel channel, int time)
        {

            var invite = await channel.CreateInviteAsync(time);
            var builder = utilsService.CreateNewEmbed("New invite", DiscordColor.Azure, invite.ToString());
            await ctx.RespondAsync(builder.Build());


        }

        [Command("clone")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleCloneChannel(CommandContext ctx, DiscordChannel channel)
        {


            var builder = utilsService.CreateNewEmbed("Clone channel", DiscordColor.Azure, $"Clone {channel.Name} channel");
            try
            {
                await channel.CloneAsync();

            }
            catch (Exception ex)
            {

                builder.Title = "Clone channel";
                builder.Description = $"ErrorClone {channel.Name} channel";
                builder.Color = DiscordColor.Red;


            }
            await ctx.RespondAsync(builder.Build());
        }
        [Command("delete-channel")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleDeleteChannel(CommandContext ctx, DiscordChannel channel)
        {


            var builder = utilsService.CreateNewEmbed("Clone channel", DiscordColor.Azure, $"Delete {channel.Name} channel");
            try
            {
                await channel.DeleteAsync();

            }
            catch (Exception ex)
            {

                builder.Title = "Delete channel";
                builder.Description = $"Delete channel {channel.Name} channel";
                builder.Color = DiscordColor.Red;


            }
            await ctx.RespondAsync(builder.Build());
        }



        [Command("get-links")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleLinks(CommandContext ctx, DiscordChannel channel)
        {
            try
            {


                string reply = "";
                IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();
                List<DiscordMessage> urls = utilsService.CheckContainsLinks(listDiscordMessages);
                urls.ToList().ForEach(message => reply += $"{message.Content} \n");


                var builder = utilsService.CreateNewEmbed("Links", DiscordColor.Azure, reply);


                await ctx.RespondAsync(builder.Build());


            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());

            }
        }

        [Command("delete-links")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleDeleteLinks(CommandContext ctx, DiscordChannel channel)
        {
            try
            {
                IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();
                List<DiscordMessage> urls = utilsService.CheckContainsLinks(listDiscordMessages);
                string reply = "";

                int count = urls.ToList().Count;
                urls.ToList().ForEach(async message => await ctx.Channel.DeleteMessageAsync(message));

                var builder = utilsService.CreateNewEmbed("Links", DiscordColor.Azure, $"Delete links  number : {count}");
                await ctx.RespondAsync(builder.Build());


            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());

            }
        }



        [Command("export-links")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleExportLinks(CommandContext ctx, DiscordChannel channel)
        {
            try
            {


                IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();
                List<DiscordMessage> urls = utilsService.CheckContainsLinks(listDiscordMessages);



                var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
                utilsService.CheckDirectory(path);


                var pathFile = Path.Join(path, "write.txt");


                IFileService fileService = new FileService();


                fileService.WriteTxt(urls, "write.txt");

                var builder = utilsService.CreateNewEmbed("Links", DiscordColor.Azure, "Export to .txt file");
                DiscordMessageBuilder builders = utilsService.SendImage(pathFile);

                await ctx.RespondAsync(builder.Build());

                builders.SendAsync(ctx.Channel);





            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());

            }
        }
        [Command("getRamInfo")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleRamInfo(CommandContext ctx, int iterate)
        {
            try
            {
                var ramInfo = ServiceInfo.GetMemeryUsedInformation(iterate);
                var builder = utilsService.CreateNewEmbed($"ram information", DiscordColor.Azure, ramInfo);
                await ctx.RespondAsync(builder.Build());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());



            }

        }
        [Command("getprocinfo")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandleProcInfo(CommandContext ctx, int iterate)
        {
            try
            {
                var ramInfo = ServiceInfo.GetProcessorPerformamceUsed(iterate);
                var builder = utilsService.CreateNewEmbed($"proc information", DiscordColor.Azure, ramInfo);
                await ctx.RespondAsync(builder.Build());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());



            }

        }
    }
}

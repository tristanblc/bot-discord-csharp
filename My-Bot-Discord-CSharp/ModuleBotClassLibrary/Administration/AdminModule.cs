﻿using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using ModuleBotClassLibrary.RessourceManager;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace ModuleBotClassLibrary
{

    public class AdminModule : BaseCommandModule
    {
        private IUtilsService utilsService { get; init; }
        private DiscordEmoji[] EmojiCache;
        private IServerInfoService ServiceInfo { get; init; }


        public AdminModule()
        {
            utilsService = new UtilsService();

            ServiceInfo = new ServerInfoService();
        }


        [Command("ban")]
        [SlashCommand("ban", null, true)]
        [DescriptionCustomAttribute("banCommand")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task ban(CommandContext ctx, DiscordMember member, string reason)
        {
            var builder = utilsService.CreateNewEmbed("Ban", DiscordColor.Green, $"{member.DisplayName} ban !");
            await member.BanAsync(1, reason);
            await ctx.RespondAsync(builder.Build());
        }





        [Command("changebotstatus")]
        [SlashCommand("changebotstatus", null, true)]
        [DescriptionCustomAttribute("botStatusCommand")]
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
        [SlashCommand("unban", null, true)]
        [DescriptionCustomAttribute("unbanCommand")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task unban(CommandContext ctx, DiscordMember member, string reason)
        {

            var builder = utilsService.CreateNewEmbed("Unban", DiscordColor.Green, $"{member.DisplayName} Unban !");
            await member.UnbanAsync(reason);
            await ctx.RespondAsync(builder.Build());
        }



        [Command("clean-all")]
        [SlashCommand("clean-all", null, true)]
        [DescriptionCustomAttribute("cleanallCommand")]
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
        [SlashCommand("clear", null, true)]
        [DescriptionCustomAttribute("clearCommand")]
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
        [SlashCommand("poll", null, true)]
        [DescriptionCustomAttribute("pollCommand")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task HandlePoll(CommandContext ctx, string question)
        {
            if (!string.IsNullOrEmpty(question))
            {
                var client = ctx.Client;
                var interactivity = client.GetInteractivity();
                if (EmojiCache == null)
                {
                   EmojiCache = new[] {
                        DiscordEmoji.FromName(client, ":white_check_mark:"),
                        DiscordEmoji.FromName(client, ":x:")
                    };
                }

                // Creating the poll message
                var pollStartText = new StringBuilder();
                pollStartText.Append("**").Append("Poll started for:").AppendLine("**");
                pollStartText.Append(question);
                var pollStartMessage = await ctx.RespondAsync(pollStartText.ToString());

                // DoPollAsync adds automatically emojis out from an emoji array to a special message and waits for the "duration" of time to calculate results.
                var pollResult = await interactivity.DoPollAsync(pollStartMessage, EmojiCache, PollBehaviour.KeepEmojis);
                var yesVotes = pollResult[0].Total;
                var noVotes = pollResult[1].Total;

                // Printing out the result
                var pollResultText = new StringBuilder();
                pollResultText.AppendLine(question);
                pollResultText.Append("Poll result: ");
                pollResultText.Append("**");
                if (yesVotes > noVotes)
                {
                    pollResultText.Append("Yes");
                }
                else if (yesVotes == noVotes)
                {
                    pollResultText.Append("Undecided");
                }
                else
                {
                    pollResultText.Append("No");
                }
                pollResultText.Append("**");
                await ctx.RespondAsync(pollResultText.ToString());
            }
            else
            {
                await ctx.RespondAsync("Error: the question can't be empty");
            }

        }


        [Command("deaf")]
        [SlashCommand("deaf", null, true)]
        [DescriptionCustomAttribute("deafCommand")]
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
        [SlashCommand("undeaf", null, true)]
        [DescriptionCustomAttribute("undeafCommand")]
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
        [SlashCommand("mute", null, true)]
        [DescriptionCustomAttribute("muteCommand")]
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
        [SlashCommand("unmute", null, true)]
        [DescriptionCustomAttribute("unmuteCommand")]
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
        [SlashCommand("addrole", null, true)]
        [DescriptionCustomAttribute("addroleCommand")]
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
        [SlashCommand("removerole", null, true)]
        [DescriptionCustomAttribute("removeCommand")]
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
        [SlashCommand("timeout", null, true)]
        [DescriptionCustomAttribute("timeoutCommand")]
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
        [SlashCommand("invite", null, true)]
        [DescriptionCustomAttribute("inviteCmd")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task inviteUser(CommandContext ctx, DiscordChannel channel, int time)
        {

            var invite = await channel.CreateInviteAsync(time);
            var builder = utilsService.CreateNewEmbed("New invite", DiscordColor.Azure, invite.ToString());
            await ctx.RespondAsync(builder.Build());


        }

        [Command("clone")]
        [SlashCommand("clone", null, true)]
        [DescriptionCustomAttribute("cloneCmd")]
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
        [SlashCommand("delete-channel", null, true)]
        [DescriptionCustomAttribute("deleteCommand")]
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
        [SlashCommand("get-links", null, true)]
        [DescriptionCustomAttribute("getlinksCommand")]
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
        [SlashCommand("delete-links", null, true)]
        [DescriptionCustomAttribute("deleteCommand")]
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
        [SlashCommand("export-links", null, true)]
        [DescriptionCustomAttribute("exportlinksCommand")]
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
        

        [Command("getprocinfo")]
        [SlashCommand("getprocinfo", null, true)]
        [DescriptionCustomAttribute("procInfoCmd")]
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
        [Command("getRamInfo")]
        [SlashCommand("ramInfoCmd", null, true)]
        [DescriptionCustomAttribute("ramInfoCmd")]
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
       
    }
}

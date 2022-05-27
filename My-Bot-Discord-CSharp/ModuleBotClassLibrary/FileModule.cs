using BotClassLibrary;
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

namespace ModuleBotClassLibrary
{
    public class FileModule : BaseCommandModule
    {

        private IUtilsService utilsService { get; set; }
        private IFileService fileService { get; set; }

        public FileModule()
        {
            fileService = new FileService();
            utilsService = new UtilsService();
        }

        [Command("messagetofile")]
        public async Task HandleMessageToFile(CommandContext ctx)
        {


            List<DiscordMessage> urls = new List<DiscordMessage>();
            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();
          


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");

            utilsService.CheckDirectory(path);    


            var pathFile = Path.Join(path, "messages.txt");


            IFileService fileService = new FileService();


            fileService.WriteTxt((List<DiscordMessage>)listDiscordMessages, "message.txt");



            var builder = utilsService.CreateNewEmbed("Message .txt", DiscordColor.Azure, "Export to .txt file");



            DiscordMessageBuilder builders = utilsService.SendImage(path);

            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);
        }


        [Command("usermessage")]
        public async Task HandleMessageToFile(CommandContext ctx,DiscordMember mem)
        {


            List<DiscordMessage> urls = new List<DiscordMessage>();
            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();


            List<DiscordMessage> discordMessages = new List<DiscordMessage>();

           foreach(var discordMessage in listDiscordMessages.ToList())
            {
                if (discordMessage.Author.Username == mem.Username)
                    discordMessages.Add(discordMessage);
            }


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
            utilsService.CheckDirectory(path);

            var pathFile = Path.Join(path, "messages.txt");

            fileService.WriteTxt(discordMessages, "message.txt");


            var builder = utilsService.CreateNewEmbed("Message .txt", DiscordColor.Azure, "Export to .txt file");



            DiscordMessageBuilder builders = utilsService.SendImage(path);


            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);
        }



    }
}

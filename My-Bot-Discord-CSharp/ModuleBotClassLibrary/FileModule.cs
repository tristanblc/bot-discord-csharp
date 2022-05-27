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


            var pathFile = Path.Join(path, $"messages_channel_{ctx.Channel.Id}.txt");


            fileService.WriteTxt(listDiscordMessages.ToList(), $"messages_channel_{ctx.Channel.Id}.txt");


            var builder = utilsService.CreateNewEmbed("Message .txt", DiscordColor.Azure, "Export to .txt file");


            DiscordMessageBuilder builders = utilsService.SendImage(pathFile);

            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);
        }


        [Command("usermessage")]
        public async Task HandleMessageToFile(CommandContext ctx,DiscordMember mem)
        {


            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();


            List<DiscordMessage> discordMessages = new List<DiscordMessage>();

           foreach(var discordMessage in listDiscordMessages.ToList())
            {
                if (discordMessage.Author.Username == mem.Username)
                    discordMessages.Add(discordMessage);
            }


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
            utilsService.CheckDirectory(path);


            var pathFile = Path.Join(path, $"messages_user_{mem.Username}.txt");


            fileService.WriteTxt(discordMessages, $"messages_user_{mem.Username}.txt");

            var builder = utilsService.CreateNewEmbed("Message .txt", DiscordColor.Azure, "Export to .txt file");


            DiscordMessageBuilder builders = utilsService.SendImage(pathFile);

            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);
        }



    }
}

using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using EffectClassLibrary;
using Microsoft.AspNetCore.Mvc.Formatters;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
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

        [Command("messagetofile")]
        public async Task HandleMessageToFile(CommandContext ctx)
        {


            List<DiscordMessage> urls = new List<DiscordMessage>();
            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();
          


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathFile = Path.Join(path, "messages.txt");


            IFileService fileService = new FileService();


            fileService.WriteTxt((List<DiscordMessage>)listDiscordMessages, "message.txt");

            var builder = new DiscordEmbedBuilder
            {
                Title = "Message .txt",

                Color = DiscordColor.Azure,
                Description = "Export to .txt file"
            };


            DiscordMessageBuilder builders = new DiscordMessageBuilder();
            FileStream fileStream = new FileStream(pathFile, FileMode.Open);
            builders.WithFile(fileStream);




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
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathFile = Path.Join(path, "messages.txt");


            IFileService fileService = new FileService();


            fileService.WriteTxt(discordMessages, "message.txt");

            var builder = new DiscordEmbedBuilder
            {
                Title = "Message .txt",

                Color = DiscordColor.Azure,
                Description = "Export to .txt file"
            };


            DiscordMessageBuilder builders = new DiscordMessageBuilder();
            FileStream fileStream = new FileStream(pathFile, FileMode.Open);
            builders.WithFile(fileStream);




            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);
        }



    }
}

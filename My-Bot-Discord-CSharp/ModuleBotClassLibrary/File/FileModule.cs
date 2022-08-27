using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using ModuleBotClassLibrary.Services;
using Newtonsoft.Json;
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
        [Description("convert messages to a txt file")]
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
        [Description("extract messages to a txt file")]
        public async Task HandleMessageToFile(CommandContext ctx, DiscordMember mem)
        {


            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();


            List<DiscordMessage> discordMessages = new List<DiscordMessage>();

            foreach (var discordMessage in listDiscordMessages.ToList())
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

        [Command("convert2zip")]
        [Description("convert messages t  zip file")]
        public async Task HandleConvert2Zip(CommandContext ctx, string? filename)
        { 
            var attachments = ctx.Message.Attachments;
            var path = fileService.Compress2Zip(attachments.ToList(), filename);

            var builder = utilsService.CreateNewEmbed("Export to .zip", DiscordColor.Azure, "Export to .zip file");


            DiscordMessageBuilder builders = utilsService.SendImage(path);

            await ctx.RespondAsync(builder.Build());
        }



        [Command("decompress2zip")]
        [Description("decomptess zip file to files")]
        public async Task HandleDecompress2Zip(CommandContext ctx, string filename)
        {

            var attachments = ctx.Message.Attachments;
            var paths = fileService.Decompress2File(attachments.ToList().First());
            var builder = utilsService.CreateNewEmbed("Export .zip to files ", DiscordColor.Azure, "Export to .zip file to files");

            await ctx.RespondAsync(builder.Build());

            paths.ForEach(path =>
            {
                DiscordMessageBuilder builders = utilsService.SendImage(path); 
                builders.SendAsync(ctx.Channel);
            }
            );    

        }



        [Command("tojson")]
        [Description("extract messages json")]
        public async Task HandleChatToJson(CommandContext ctx)
        {

            IEnumerable<DiscordMessage> listDiscordMessages = await ctx.Channel.GetMessagesAsync();


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");

            utilsService.CheckDirectory(path);


            var pathFile = Path.Join(path, $"messages_channel_{ctx.Channel.Id}.json");


            fileService.WriteJson(listDiscordMessages.ToList(), $"messages_channel_{ctx.Channel.Id}.json");

            var builder = utilsService.CreateNewEmbed("Export json", DiscordColor.Azure, "Export to json");


            DiscordMessageBuilder builders = utilsService.SendImage(pathFile);

            await ctx.RespondAsync(builder.Build());

            builders.SendAsync(ctx.Channel);


        }

    }

}

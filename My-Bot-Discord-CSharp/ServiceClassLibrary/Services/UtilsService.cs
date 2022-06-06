using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class UtilsService : IUtilsService
    {
  

        public List<DiscordMessage> CheckContainsLinks(IEnumerable<DiscordMessage> listDiscordMessages)
        {

            List<DiscordMessage> urls = new List<DiscordMessage>();
            foreach (DiscordMessage message in listDiscordMessages)
            {
                if (message == null)
                    continue;
                else
                {
                    var messageString = message.Content.ToString();
                    Match url = Regex.Match(messageString, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
                    Console.WriteLine(url);
                    if (url.Success)
                        urls.Add(message);

                }
            }
            return urls;

        }

        public void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

       
        public DiscordEmbedBuilder CreateNewEmbed(string title, DiscordColor color, string description)
        {

            var builder = new DiscordEmbedBuilder
            {
                Title = title,

                Color = color,
                Description = description
            };

            return builder;
        }


        public void DeleteDirectoryIfExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path) ;
        }

        public string GetFilePathFilter(string path, DiscordAttachment attachment)
        {
           
            var pathFileFilter = Path.Join(path, $"{ attachment.FileName }_changed.png");
            return pathFileFilter;
        }

        public List<string> GetFiles(string path)
        {
            return Directory.GetFiles(path).ToList();           
        }

        public bool isJson(string message)
        {
            string input = message.ToString();


            if (input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]"))
                return true;

            return false;
        }

        public DiscordMessageBuilder SendImage(string path)
        {
            DiscordMessageBuilder builders = new DiscordMessageBuilder();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            builders.WithFile(fileStream);
            return builders;
           
        }

        public void SendResultat(CommandContext ctx,string pathFileFilter)
        {

            var embed_info = this.CreateNewEmbed("Add filter to image", DiscordColor.Azure, "Add filter ...");

            ctx.RespondAsync(embed_info.Build());
  
            var embed = this.SendImage(pathFileFilter);

            embed.SendAsync(ctx.Channel);

        }
    }
}

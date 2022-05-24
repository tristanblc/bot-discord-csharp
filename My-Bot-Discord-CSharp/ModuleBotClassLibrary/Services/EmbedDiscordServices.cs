using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary.Services
{
    internal class EmbedDiscordServices
    {

        private DiscordMessage DiscordMessage { get; init; }

        private DiscordEmbedBuilder DiscordEmbedBuilder { get; init; }

        private DiscordEmbedThumbnail DiscordEmbedThumbnail { get; init; }

        private DiscordMessageActivity DiscordMessageActivity { get; init; }

        public EmbedDiscordServices(DiscordMessage discordMessage, DiscordMessageActivity discordMessageActivity)
        {
            DiscordMessage = discordMessage;
            DiscordMessageActivity = discordMessageActivity;       
            
        }
        public EmbedDiscordServices(DiscordMessage discordMessage, DiscordMessageActivity discordMessageActivity, DiscordEmbedThumbnail discordEmbedThumbnail) : this(discordMessage, discordMessageActivity)
        {
            DiscordEmbedThumbnail = discordEmbedThumbnail;

        }


        public DiscordEmbed CreateEmbedMessage(string title, string descritpion, string message, string activity,string author)
        {
            DiscordEmbedBuilder discordMessage = new DiscordEmbedBuilder();
            discordMessage.Title = title;
            discordMessage.Description = descritpion;
            discordMessage.Timestamp = DateTime.Now.ToLocalTime();      

            return discordMessage;

        }






    }
}

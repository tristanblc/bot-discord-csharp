using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IUtilsService
    {

        DiscordMessageBuilder SendImage(string path);

        string GetFilePathFilter(string path, DiscordAttachment attachment);

       
        DiscordEmbedBuilder CreateNewEmbed(string title, DiscordColor color, string description);

        void DeleteDirectoryIfExist(string path);

        void DeleteFile(string path);


        void CheckDirectory(string path);


        void SendResultat(CommandContext ctx, string pathFileFilter);

        List<DiscordMessage> CheckContainsLinks(IEnumerable<DiscordMessage> message);

        
       List<string> GetFiles(string path);


        bool isJson(string message);


    }
}

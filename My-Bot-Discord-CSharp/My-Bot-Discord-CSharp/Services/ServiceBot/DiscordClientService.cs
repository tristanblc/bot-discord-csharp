using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using ModuleBotClassLibrary;

using My_Bot_Discord_CSharp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.ServiceBot
{
    public class DiscordClientService: IDiscordClientService
    {
        public DiscordClientService()
        {

        }
        public DiscordClient CreateDiscordClient()
        {

            Startup startup = new Startup();

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = startup.GetTokenFromJsonFile(),
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
              
            });



            var command_configuration = new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            };


            var commands = discord.UseCommandsNext(command_configuration);


            commands.RegisterCommands<InfoModule>();
            commands.RegisterCommands<OtherToolsModule>();
            commands.RegisterCommands<AdminModule>();          
            commands.RegisterCommands<MusicModule>();
            commands.RegisterCommands<FilmModule>();
            commands.RegisterCommands<FileModule>();
           


            return discord;
        }





   


    }
}

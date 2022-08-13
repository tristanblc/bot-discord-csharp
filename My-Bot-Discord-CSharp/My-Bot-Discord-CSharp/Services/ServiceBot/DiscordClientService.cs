using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using ModuleBotClassLibrary;
using ModuleBotClassLibrary.Fun;
using My_Bot_Discord_CSharp.Formatter;
using My_Bot_Discord_CSharp.Services.Interface;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.ServiceBot
{
    public class DiscordClientService: IDiscordClientService
    {
        private ILoggerProject LoggerProject { get; init; }
        public DiscordClientService()
        {
            LoggerProject = new LoggerProject();
        }
        public DiscordClient CreateDiscordClient()
        {

            Startup startup = new Startup();
            LoggerProject.WriteInformationLog("Discord client initialize..");

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

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromDays(25)
            });

            var commands = discord.UseCommandsNext(command_configuration);


            commands.RegisterCommands<InfoModule>();
            commands.RegisterCommands<OtherToolsModule>();
            commands.RegisterCommands<AdminModule>();          
            commands.RegisterCommands<MusicModule>();
            commands.RegisterCommands<FilmModule>();
            commands.RegisterCommands<FileModule>();
            commands.RegisterCommands<ImageModule>();
            commands.RegisterCommands<GamesModule>();
            commands.RegisterCommands<VideoModule>();
            commands.RegisterCommands<TicketModule>();
            commands.RegisterCommands<RappelModule>();
            commands.RegisterCommands<RedditModule>();
            commands.RegisterCommands<SteamModule>();

            commands.SetHelpFormatter<BotHelpFormatter>();

            LoggerProject.WriteInformationLog("Discord client created");

            return discord;
        }

    }
}

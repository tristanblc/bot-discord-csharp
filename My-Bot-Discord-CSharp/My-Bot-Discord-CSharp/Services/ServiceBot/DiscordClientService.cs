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

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(30)
            });

            var commands = discord.UseCommandsNext(command_configuration);

            DiscordActivity discordActivity = new DiscordActivity();
            discordActivity.StreamUrl = "https://github.com/tristanblc"; 
        
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
         
            discord.UpdateStatusAsync(discordActivity);

            return discord;
        }

    }
}

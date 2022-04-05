// See https://aka.ms/new-console-template for more information
using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using ModuleBotClassLibrary;



static void Main(string[] args)
{

    MainAsync().GetAwaiter().GetResult();
}

static async Task MainAsync()
{


    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = "",
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.All
    });

    var command_configuration = new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    };

    var endpoint = new ConnectionEndpoint
    {
        Hostname = "127.0.0.1", // From your server configuration.
        Port = 2333 // From your server configuration
    };

    var lavalinkConfig = new LavalinkConfiguration
    {
        Password = "", // From your server configuration.
        RestEndpoint = endpoint,
        SocketEndpoint = endpoint
    };

    var lavalink = discord.UseLavalink();


    var commands = discord.UseCommandsNext(command_configuration);


    commands.RegisterCommands<InfoModule>();
    commands.RegisterCommands<OtherToolsModule>();
    commands.RegisterCommands<AdminModule>();
    commands.RegisterCommands<BusInfoModule>();
    commands.RegisterCommands<MusicModule>();
    await discord.ConnectAsync();
    await lavalink.ConnectAsync(lavalinkConfig);
    await Task.Delay(-1);
}

Main(args);

Console.ReadLine();
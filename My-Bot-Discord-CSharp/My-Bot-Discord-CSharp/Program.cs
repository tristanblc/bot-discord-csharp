// See https://aka.ms/new-console-template for more information
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ModuleBotClassLibrary;

Console.WriteLine("Hello, World!");

static void Main(string[] args)
{

    MainAsync().GetAwaiter().GetResult();
}

static async Task MainAsync()
{

    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = ,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged
    });

    var command_configuration = new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    };

    

    var commands = discord.UseCommandsNext(command_configuration);


    commands.RegisterCommands<InfoModule>();
    commands.RegisterCommands<OtherToolsModule>();
    commands.RegisterCommands<AdminModule>();

    await discord.ConnectAsync();
    await Task.Delay(-1);

}

Main(args);

Console.ReadLine();
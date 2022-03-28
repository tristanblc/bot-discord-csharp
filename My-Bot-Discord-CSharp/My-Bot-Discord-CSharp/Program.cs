// See https://aka.ms/new-console-template for more information
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
        Token = "OTUwNzUxNTcyMTA5OTU1MTQz.YideZg.Gq3aUr6q8kJgJ7bsZOjBLNe0y10",
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
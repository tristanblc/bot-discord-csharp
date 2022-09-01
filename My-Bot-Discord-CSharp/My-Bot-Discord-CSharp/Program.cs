// See https://aka.ms/new-console-template for more information
using BotClassLibrary;
using BotClassLibrary.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using ModuleBotClassLibrary;
using My_Bot_Discord_CSharp.Services;
using My_Bot_Discord_CSharp.Services.Interface;
using My_Bot_Discord_CSharp.Services.ServiceBot;
using System.Reflection;
using System.Resources;



static void Main(string[] args)
{
    MainAsync().GetAwaiter().GetResult();   
}

static async Task MainAsync()
{

    
    
    var discord_service = new DiscordClientService();
 
    try
    {
          
        var discord = discord_service.CreateDiscordClient();

        var endpoint = new ConnectionEndpoint
        {
            Hostname = "127.0.0.1", // From your server configuration.
            Port = 2333, // From your server configuration
        };

        var lavalinkConfig = new LavalinkConfiguration
        {           
            Password = "motdepasse", // From your server configuration.
            RestEndpoint = endpoint,
            SocketEndpoint = endpoint
        };


        var act = new DiscordActivity("Les tutos de chef michel - !help",ActivityType.Watching);
       

        var lavalink = discord.UseLavalink();

        await discord.ConnectAsync(act);
        lavalink.ConnectAsync(lavalinkConfig);

        await Task.Delay(-1);      

    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }



}

Main(args);

Console.ReadLine();
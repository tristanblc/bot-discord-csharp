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
using My_Bot_Discord_CSharp.Services.ServiceLavalink;

static void Main(string[] args)
{
    try
    {
        MainAsync().GetAwaiter().GetResult();
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
        return;

    }

   
}

static async Task MainAsync()
{

    var discord_service = new DiscordClientService();


    var discord = new DiscordClient(new DiscordConfiguration());
    try
    {
        discord = discord_service.CreateDiscordClient();

    }catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
        return;
    }

    await discord.ConnectAsync();

    await Task.Delay(-1);

}

Main(args);

Console.ReadLine();
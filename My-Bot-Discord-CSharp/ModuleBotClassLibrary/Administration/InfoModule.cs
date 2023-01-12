using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using ModuleBotClassLibrary.RessourceManager;
using ReaderClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ModuleBotClassLibrary
{
    public class InfoModule : BaseCommandModule
    {

        private IUtilsService utilsService { get; set; }
        
        private readonly HttpClient HttpClient = new HttpClient();

        private readonly string Url = "https://calendrier.api.gouv.fr/jours-feries/metropole/";

        public JourFerieService FerieService { get; init; }

        public InfoModule()
        {

            Url = Url + DateTime.Now.Year.ToString() + ".json";
            FerieService = new JourFerieService(HttpClient, Url);
            utilsService = new UtilsService();

        }


        [Command("ping")]
        [SlashCommand("ping", null, false)]
        [DescriptionCustomAttribute("pingsenderCommand")]

        public async Task PingCommand(CommandContext ctx)
        {
            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Red, "");
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send("localhost", 1000);
                if (reply != null)
                {
                    builder.Description = $"Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString();


                    await ctx.RespondAsync(builder.Build());
                    return;
                }

                builder.Description = "Error";
            }
            catch
            {
                builder.Description = "Timeout!";
              
            }
            Console.ReadKey();


            await ctx.RespondAsync(builder.Build());
        }      




        [Command("pingweb")]
        [SlashCommand("pingweb", null, false)]
        [DescriptionCustomAttribute("pingwebCommand")]
        public async Task PingWebsiteCommand(CommandContext ctx, string url)
        {
            var builder = utilsService.CreateNewEmbed("Status", DiscordColor.Red, "");
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(url, 1000);
                if (reply != null)
                {
                    builder.Description = $"Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString();


                    await ctx.RespondAsync(builder.Build());
                    return;
                }

                builder.Description = "Error";
            }
            catch
            {
                builder.Description = "Timeout!";

            }
            Console.ReadKey();


            await ctx.RespondAsync(builder.Build());
        }

    }

}
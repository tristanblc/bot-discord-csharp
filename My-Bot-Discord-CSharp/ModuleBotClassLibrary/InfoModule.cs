using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ReaderClassLibrary.Services;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ModuleBotClassLibrary
{
    public class InfoModule : BaseCommandModule
    {



         
        private readonly HttpClient HttpClient = new HttpClient();

        private readonly string Url = "https://calendrier.api.gouv.fr/jours-feries/metropole/";

        public JourFerieService FerieService { get; init; }

        public InfoModule()
        {

            Url = Url + DateTime.Now.Year.ToString() + ".json";
            FerieService = new JourFerieService(HttpClient, Url);
        }

   
        [Command("ping")]
        public async Task PingCommand(CommandContext ctx)
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send("localhost", 1000);
                if (reply != null)
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Status",

                        Color = DiscordColor.Azure,
                        Description = $"Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString()
                    };


                    await ctx.RespondAsync(builder.Build());
                    return;
                }

                await ctx.RespondAsync("Error");
            }
            catch
            {
                await ctx.RespondAsync("Timeout!");
            }
            Console.ReadKey();
        }      




        [Command("pingweb")]
        public async Task PingWebsiteCommand(CommandContext ctx, string url)
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(url, 1000);
                if (reply != null)
                {

                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Status",

                        Color = DiscordColor.Azure,
                        Description = $"Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString()
                    };


                    await ctx.RespondAsync(builder.Build());
     
                    return;


                }
                await ctx.RespondAsync("Error");

            }
            catch(Exception ex)
            {
                await ctx.RespondAsync("Error");

            }
        }


        [Command("nextferie")]
        public async Task JourFerieCommand(CommandContext ctx)
        {
            try
            {

                var reply =  await FerieService.GetApprochDateTime();
                if(reply == null)
                {
                    throw new NullReferenceException("Empty");
                }



                string reply_ = $" Prochain jour férie  :  { reply.DateTime}  ";


                await ctx.RespondAsync(reply_);

            }
            catch (Exception ex)
            {
                await ctx.RespondAsync(ex.ToString());

            }
        }



      






    }

}
using BotClassLibrary;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using ModuleBotClassLibrary.RessourceManager;
using ReaderClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class RappelModule : BaseCommandModule
    {

        private IUtilsService utilsService { get; set; }

        private RappelService RappelService { get; init; }

        public RappelModule()
        {
            utilsService = new UtilsService();

            var builder = new ConfigurationBuilder()
                                .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            var url = config.GetSection("apiUrl").Value.ToString() + "/Rappel";

            RappelService = new RappelService(new HttpClient(), url);

        }

        [Command("create-rappel")]
        [DescriptionCustomAttribute("createRappelCmd")]       
        public async Task HandleCreateRappel(CommandContext ctx,string name,string description)
        {
            try
            {
                Rappel rappel = new Rappel(name, description, ctx.Member.Username, DateTime.Now, ctx.Member.Id) ;

                rappel.Id = new Guid();

                await RappelService.Add(rappel);


                var builder = utilsService.CreateNewEmbed("New Rappel", DiscordColor.Azure, "Your new rappel is save - See you later");

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error Rappel", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }


        [Command("checked-rappel")]
        [DescriptionCustomAttribute("checkRappelCmd")]
        public async Task HandleCheckedRappel(CommandContext ctx,string id)
        {
            try
            {

                Guid guid = new Guid(id);
                var ticket = RappelService.Get(guid).Result;
                ticket.IsRead = true;

                var result = RappelService.Update(ticket).Result;

                if (result != null)
                {
                    var builder = utilsService.CreateNewEmbed("Checked Rappel", DiscordColor.Azure, $"Your  rappel is Checked - See you later");

                    await ctx.RespondAsync(builder.Build());
                }


            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }



        }

        [Command("list-rappel")]
        [DescriptionCustomAttribute("listRappelCmd")]
        public async Task HandleLisUnreadTicket(CommandContext ctx)
        {
            try
            {
                var memberId = ctx.Member.Id;


                var builder = utilsService.CreateNewEmbed("List of your rappel", DiscordColor.Azure, $"List of {ctx.User.Username } rappels --\n");

                var tickets = RappelService.GetAll().Result;

                var userRappel = new List<Rappel>();
                    
                    
                tickets.ToList().ForEach(rappel =>
                {

                    if (rappel.UserDiscordId  == memberId) 
                        userRappel.Add(rappel);
                });



                userRappel.ToList().ForEach(ticket =>
                {
                    if(ticket.IsRead == false)
                        builder.Description += $"Id: {ticket.Id} - Member : {ticket.DiscordMember}";
                });
                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error List Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }

        [Command("read-rappel")]
        [DescriptionCustomAttribute("readRappelCmd")]
        public async Task HandleLisUnreadRappel(CommandContext ctx, string id)
        {
            try
            {
                Guid guid = new Guid(id);

                var builder = utilsService.CreateNewEmbed("Read a Rappel", DiscordColor.Azure, "");

                var ticket = RappelService.Get(guid).Result;



                builder.Description += $" - Id: {ticket.Id} ";
                builder.Description += $" \n - Member: {ticket.DiscordMember}";
                builder.Description += $" \n - Description: {ticket.Description} ";


                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error List Rappel", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }




    }
}

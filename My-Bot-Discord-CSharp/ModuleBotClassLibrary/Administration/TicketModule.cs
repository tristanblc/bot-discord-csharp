using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using ModuleBotClassLibrary.RessourceManager;
using ModuleBotClassLibrary.Services;
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
    public class TicketModule : BaseCommandModule
    {
         private IUtilsService utilsService { get; set; }

         private TicketService TicketService { get; init; }

        public TicketModule()
        {
            utilsService = new UtilsService();

            var builder = new ConfigurationBuilder()
                                .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            var url = config.GetSection("apiUrl").Value.ToString() + "/Ticket";

            TicketService = new TicketService(new HttpClient(), url);
        
       
        }

 
        [Command("create-ticket")]
        [DescriptionCustomAttribute("createticketCommand")]
        public async Task HandleCreateTicket(CommandContext ctx,DiscordMember member,string title,string description)
        {

            try
            {
                Ticket new_ticket = new Ticket(title, description, DateTime.Now, member.Username);

                new_ticket.Id = new Guid();

                TicketService.Add(new_ticket);


                var builder = utilsService.CreateNewEmbed("New Ticket", DiscordColor.Azure, "Your new Ticket is save - See you later");

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }
          
        }

        [RequirePermissions(Permissions.Administrator)]
        [Command("checked-ticket")]
        [DescriptionCustomAttribute("checkTicketCommand")]
        public async Task HandleDeleteTicket(CommandContext ctx, string id)
        {


            try
            {

                Guid guid = new Guid(id);

                var ticket = TicketService.Get(guid).Result;
                ticket.IsRead = true;

                var result = TicketService.Update(ticket).Result;

                if (result != null)
                {
                    var builder = utilsService.CreateNewEmbed("Delete Ticket", DiscordColor.Azure, $"Your  Ticket is checked  - See you later");

                    await ctx.RespondAsync(builder.Build());
                }
               

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }

        [RequirePermissions(Permissions.Administrator)]
        [Command("list-unread-ticket")]
        [DescriptionCustomAttribute("listticketCommand")]
        public async Task HandleLisUnreadTicket(CommandContext ctx)
        {
            try
            {

              

                var builder = utilsService.CreateNewEmbed("List of tickets", DiscordColor.Azure, "List of users tickets\n");

                var tickets = TicketService.GetAll().Result;

              

                tickets.ToList().ForEach(ticket =>
                {
                    if(ticket.IsRead)
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

        [RequirePermissions(Permissions.Administrator)]
        [Command("read-ticket")]
        public async Task HandleReadTicket(CommandContext ctx,  string id)
        {
            try
            {
                Guid guid = new Guid(id);

                var builder = utilsService.CreateNewEmbed("Read a ticket", DiscordColor.Azure, "");

                var ticket = TicketService.Get(guid).Result;    


                    
                builder.Description += $" - Id: {ticket.Id} ";
                builder.Description += $" \n - Member: {ticket.DiscordMember}";
                builder.Description += $" \n - Title: {ticket.Title} ";
                builder.Description += $" \n - Description: {ticket.Description} ";
            

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error List Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }


        [RequirePermissions(Permissions.Administrator)]
        [Command("check-ticket")]
        public async Task HandleDealTicket(CommandContext ctx, string id)
        {
            try
            {

                Guid guid = new Guid(id);

                var ticket = TicketService.Get(guid).Result;
                ticket.IsDeal = true;
                ticket.IsRead = true;

                TicketService.Update(ticket);


                var builder = utilsService.CreateNewEmbed("Deal Ticket", DiscordColor.Azure, "Ticket is checked - See you later");

                await ctx.RespondAsync(builder.Build());

            }
            catch (Exception ex)
            {
                var builder = utilsService.CreateNewEmbed("Error Ticket", DiscordColor.Red, "Try again");

                await ctx.RespondAsync(builder.Build());


            }

        }


    }
}

using BotClassLibrary;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
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
                                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            var url = config.GetSection("apiUrl").Value.ToString() + "Ticket";

            TicketService = new TicketService(new HttpClient(), url);
        
       
        }

 
        [Command("create-ticket")]
        public async Task HandleCreateTicket(CommandContext ctx,DiscordMember member,string title,string description)
        {

            try
            {
                Ticket new_ticket = new Ticket(title, description, DateTime.Now, member.Username);

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
        [Command("delete-ticket")]
        public async Task HandleDeleteTicket(CommandContext ctx, Guid id)
        {


            try
            {

                Ticket ticket = await TicketService.Get(id);

                TicketService.Delete(ticket);


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
        [Command("list-unread-ticket")]
        public async Task HandleLisUnreadTicket(CommandContext ctx, string? filename)
        {
            try
            {
                var builder = utilsService.CreateNewEmbed("New Ticket", DiscordColor.Azure, "Your new Ticket is save - See you later");

                var tickets = await TicketService.GetAll();

                tickets.ToList().ForEach(ticket =>
                {
                    if(ticket.IsRead  == false)
                        builder.Description += $"Id: {ticket.Id} - Member : {ticket.DiscordMember}  -  Description : {ticket.Description}";
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
        [Command("deal-ticket")]
        public async Task HandleDealTicket(CommandContext ctx, Guid id)
        {
            try
            {

                Ticket ticket = await TicketService.Get(id);
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

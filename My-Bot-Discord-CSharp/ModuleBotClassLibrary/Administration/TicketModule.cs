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

        private readonly HttpClient httpClient = ;
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
        public async Task HandleCreateTicket(CommandContext ctx,DiscordMember warnMember,string description)
        {





        }


        [Command("delete-ticket")]
        public async Task HandleDeleteTicket(CommandContext ctx)
        {


          
        }

        [Command("list-unread-ticket")]
        public async Task HandleLisUnreadTicket(CommandContext ctx, string? filename)
        {
           
        }



        [Command("deal-ticket")]
        public async Task HandleDealTicket(CommandContext ctx, string filename)
        {

         

        }


    }
}

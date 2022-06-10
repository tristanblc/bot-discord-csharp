using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    internal class RappelModule : BaseCommandModule
    {
        private IUtilsService utilsService { get; set; }

        public RappelModule()
        {
            utilsService = new UtilsService();

        }

        [Command("create-rappel")]
        public async Task HandleCreateRappel(CommandContext ctx)
        {


        }


        [Command("delete-rappel")]
        public async Task HandleDeleteRappel(CommandContext ctx)
        {



        }

        [Command("list-my-rappel")]
        public async Task HandleLisUnreadRappel(CommandContext ctx)
        {

        }



        [Command("deal-rappel")]
        public async Task HandleDealRappel(CommandContext ctx)
        {



        }

    }
}

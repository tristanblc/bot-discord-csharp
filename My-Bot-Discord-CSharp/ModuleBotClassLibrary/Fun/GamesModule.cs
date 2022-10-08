using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using GamesClassLibrary;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Collections;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using ModuleBotClassLibrary.RessourceManager;

namespace ModuleBotClassLibrary
{   
    public  class GamesModule : BaseCommandModule
    {


        private IUtilsService utilsService { get; set; }

        public GamesModule()
        {


        }

        [Command("pileorface")]
        public async Task HandlePileOrFace(CommandContext ctx)
        {
            PileFace pileorface = new PileFace(ctx);
            pileorface.PlayGame();
        }


        [Command("rockpaperscissor")]
        public async Task HandleRockPaperScissore(CommandContext ctx)
        {
            RockPaperScissors game = new RockPaperScissors(ctx);
            game.PlayRockScissor();
        }

        [Command("justprice")]
        public async Task HandleJustePrice(CommandContext ctx)
        {
            JustPrice juste = new JustPrice(ctx);
            juste.PlayGame();
        }


    }
}

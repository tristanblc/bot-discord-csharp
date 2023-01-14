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
using DSharpPlus.SlashCommands;

namespace ModuleBotClassLibrary
{   
    public  class GamesModule : BaseCommandModule
    {


        private IUtilsService utilsService { get; set; }

        public GamesModule()
        {


        }

        [Command("pileorface")]
        [SlashCommand("pileorface", null, false)]
        public async Task HandlePileOrFace(CommandContext ctx)
        {
            PileFace pileorface = new PileFace(ctx);
            pileorface.PlayGame();
        }


        [Command("rockpaperscissor")]
        [SlashCommand("rockpaperscissor", null, false)]
        public async Task HandleRockPaperScissore(CommandContext ctx)
        {
            RockPaperScissors game = new RockPaperScissors(ctx);
            game.PlayRockScissor();
        }

        [Command("justprice")]
        [SlashCommand("justprice", null, false)]
        public async Task HandleJustePrice(CommandContext ctx)
        {
            JustPrice juste = new JustPrice(ctx);
            juste.PlayGame();
        }


    }
}

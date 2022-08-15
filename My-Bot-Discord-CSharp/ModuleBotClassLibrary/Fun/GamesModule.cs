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

namespace ModuleBotClassLibrary
{   
    public  class GamesModule : BaseCommandModule
    {


        private IUtilsService utilsService { get; set; }

        public GamesModule()
        {


        }

        [Command("pileouface")]
        [Description("Pile ou face")]
        public async Task PileOuFaceCommand(CommandContext ctx, string choice)
        {
            PileFace pileFace = new PileFace(ctx,choice);
            pileFace.PlayGame();
        }

   

    }
}

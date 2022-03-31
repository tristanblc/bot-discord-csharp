using BusClassLibrary;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ReaderClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
   public class BusInfoModule : BaseCommandModule
    {

        [Command("arret")]
        public async Task arretCommand(CommandContext ctx,string name)
        {
            var service = new ArretService(new HttpClient(), "https://localhost:7167/api/Arret/");

            try
            {
                Arret arret = await service.Get();

                await ctx.RespondAsync($"Nom arret : " + arret.stop_name.ToString() + " Ville : " + arret.ville.ToString());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }
        }

        [Command("arrets")]
        public async Task arretsCommand(CommandContext ctx)
        {
            var service = new ArretService(new HttpClient(), "https://localhost:7167/api/Arret/All");

            try
            {
                var _arrets = (await service.GetAll()).ToList();
                await ctx.RespondAsync($"Les  arrets /n");
                foreach (var arret in _arrets)
                {
                    await ctx.RespondAsync($"Nom arret : " + arret.stop_name.ToString() + " Ville : " + arret.ville.ToString());
                   
                }
   
                    
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }
        }

        [Command("lignes")]
        public async Task lignesCommand(CommandContext ctx)
        {
            var service = new LigneService(new HttpClient(), "https://localhost:7167/api/Ligne/All");

            try
            {
                var _arrets = (await service.GetAll()).ToList();
                await ctx.RespondAsync($"Les lignes /n");

                foreach (var arret in _arrets)
                {
                    await ctx.RespondAsync($"Nom ligne : " + arret.route_short_name + " Ville : " + arret.route_desc.ToString());

                }


            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }
        }


        [Command("ligne")]
        public async Task ligneCommand(CommandContext ctx, string name)
        {
            var service = new LigneService(new HttpClient(), "https://localhost:7167/api/Ligne/");

            try
            {
                Ligne arret = await service.Get();

                await ctx.RespondAsync($"Nom ligne : " + arret.route_short_name + " Ville : " + arret.route_desc.ToString());
            }
            catch (Exception ex)
            {
                await ctx.RespondAsync("erreur");

            }
        }





    }
}

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
    public class FilmModule : BaseCommandModule
    {
        private IUtilsService utilsService { get; set; }

        private readonly string urlBase = "https://api.tvmaze.com/";

        private  readonly HttpClient httpClient = new HttpClient();

        public FilmModule()
        {
            utilsService = new UtilsService();
        }

        [Command("film")]
        public async Task FilmCommand(CommandContext ctx, string message)
        {


            var url = urlBase + "singlesearch/shows?q=" + message;

            FilmService filmService = new FilmService(httpClient,url);


            var film = await filmService.Get();


            var return_value = $"Name : { film.name }";
            return_value += $"\nLangage : { film.status }";
                
            return_value += $"\nFirst premiere : { film.premiered.ToString() }";

            return_value += $"\n Link { film.url }";

          
            var builder = utilsService.CreateNewEmbed("Film", DiscordColor.Red, return_value);
            await ctx.RespondAsync(builder.Build());
        }




        [Command("star")]
        public async Task PeopleCommand(CommandContext ctx, string message)
        {

            var url = urlBase + "search/people?q=" + message;

            FilmPeopleService filmService = new FilmPeopleService(httpClient, url);


            var film = await filmService.Get();


            var return_value = $"Name { film.person.name}  ";

            return_value += $"\n {film.person.image.medium.ToString()}";

          

            var builder = utilsService.CreateNewEmbed("Star", DiscordColor.Red, return_value);
            await ctx.RespondAsync(builder.Build());
        }
    }
}

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ReaderClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleBotClassLibrary
{
    public class FilmModule : BaseCommandModule
    {

        private readonly string urlBase = "https://api.tvmaze.com/";

        private  readonly HttpClient httpClient = new HttpClient();

        [Command("film")]
        public async Task FilmCommand(CommandContext ctx, string message)
        {

            var url = urlBase + "singlesearch/shows?q=" + message;

            FilmService filmService = new FilmService(httpClient,url);


            var film = await filmService.Get();


            var return_value = $"Name : { film.name } Langage : { film.status }  First premiere : { film.premiered.ToString() }";


            return_value += $"\n Link { film.url }";


            await ctx.RespondAsync(return_value);
        }




        [Command("people")]
        public async Task PeopleCommand(CommandContext ctx, string message)
        {

            var url = urlBase + "search/people?q=" + message;

            FilmPeopleService filmService = new FilmPeopleService(httpClient, url);


            var film = await filmService.Get();


            var return_value = $"Name { film.person.name}  ";


            return_value += $"\n Birthday { film.person.birthdate.ToString() }";
            if (film.person.deathdate != null)
                return_value += $"\n Birthday { film.person.deathdate.ToString() }";

            return_value += $"\n {film.person.image.medium.ToString()}";

            await ctx.RespondAsync(return_value);
        }
    }
}

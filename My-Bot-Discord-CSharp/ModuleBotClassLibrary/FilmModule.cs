using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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


            var return_value = $"Name : { film.name }";
            return_value += $"\nLangage : { film.status }";
                
            return_value += $"\nFirst premiere : { film.premiered.ToString() }";

            return_value += $"\n Link { film.url }";

            var builder = new DiscordEmbedBuilder
            {
                Title = "Film",

                Color = DiscordColor.Azure,

                Description = return_value

            };

            await ctx.RespondAsync(builder.Build());
        }




        [Command("star")]
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

            var builder = new DiscordEmbedBuilder
            {
                Title = "Star",

                Color = DiscordColor.Azure,

                Description = return_value

            };

            await ctx.RespondAsync(builder.Build());
        }
    }
}

using BotClassLibrary;
using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Services
{
    public  class FilmService :GenericApiReader<Film>, IGenericInterface<Film>
    {
        public FilmService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

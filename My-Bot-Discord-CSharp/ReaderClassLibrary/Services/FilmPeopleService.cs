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
    public class FilmPeopleService : GenericApiReader<People>, IGenericInterface<People>
    {
        public FilmPeopleService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

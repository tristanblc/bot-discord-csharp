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
    public class DogService : GenericApiReader<Dogs>, IAnimalService<Dogs>
    {
        public DogService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

using BusClassLibrary;
using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Services
{
    public class ArretService : GenericApiReader<Arret>, IGenericInterface<Arret>
    {

        public ArretService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

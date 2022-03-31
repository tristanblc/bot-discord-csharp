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
    public class LigneService : GenericApiReader<Ligne>, IBusInfoService<Ligne>
    {
        public LigneService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

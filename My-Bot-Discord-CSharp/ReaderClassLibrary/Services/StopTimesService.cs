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
    public class StopTimesService : GenericApiReader<StopTimes> , IBusInfoService<StopTimes>
    {
        public StopTimesService(HttpClient client, string url) : base(client, url)
        {
                
        }
    }
}

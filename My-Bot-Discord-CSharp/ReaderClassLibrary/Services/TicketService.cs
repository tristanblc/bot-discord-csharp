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
    public class TicketService : GenericApiReader<Ticket>, IGenericInterface<Ticket>
    {
        public TicketService(HttpClient httpClient, string baseuri) : base(httpClient, baseuri)
        {
        }
    }
}

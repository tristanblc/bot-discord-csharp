using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDTOClassLibrary
{
    public  class StopTimesDto : BaseEntity
    {
        public int stop_id { get; set; }
        public ulong stop_sequence { get;  set; }
        public DateTime arrival_time { get; set; }
        public DateTime departure_time { get;  set; }
    }
}

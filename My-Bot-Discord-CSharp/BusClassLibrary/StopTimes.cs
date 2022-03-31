using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusClassLibrary
{
    public class StopTimes : BaseEntity
    {

         public int stop_id { get; private set; }
         public ulong stop_sequence { get; private set; }
         public DateTime arrival_time { get; private set; }
         public DateTime departure_time { get; private set; }

        public StopTimes(int stopId, DateTime arrivaltime, DateTime departuretime, ulong stop_sequence)
        {
            this.stop_id = stopId;  
            this.arrival_time = arrivaltime;
            this.stop_sequence = stop_sequence;
            this.departure_time = departuretime;   
        }

    }
}

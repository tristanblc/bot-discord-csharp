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
         public float stop_sequence { get; private set; }
         public DateTime arrival_time { get; private set; }
         public DateTime departure_time { get; private set; }

        public StopTimes(int stop_id, DateTime departure_time, DateTime arrival_time, float stop_sequence)
        {
            this.stop_id = stop_id;  
            this.arrival_time = arrival_time;
            this.stop_sequence = stop_sequence;
            this.departure_time = departure_time;   
        }

    }
}

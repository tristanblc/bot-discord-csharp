using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDTOClassLibrary
{
   public class TripDto : BaseEntity
    {
        public Guid service_id { get; set; }

        public string trip_headsign { get;  set; }
        public int direction_id { get;  set; }
        public Guid shape_id { get;  set; }

    }
}

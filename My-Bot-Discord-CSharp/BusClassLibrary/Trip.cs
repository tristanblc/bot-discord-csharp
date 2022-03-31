using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusClassLibrary
{
    public  class Trip : BaseEntity
    {
        public Guid service_id { get; private set; }
        
        public string trip_headsign { get; private set; }
        public int direction_id { get; private set; }
        public Guid shape_id { get; private set; }


        public Trip(Guid service_id , string trip_headsign, int direction_id, Guid shape_id)
        {
            Id = Guid.NewGuid(); 
            this.service_id = service_id;
            this.trip_headsign = trip_headsign;
            this.direction_id = direction_id;
        }
    }
}

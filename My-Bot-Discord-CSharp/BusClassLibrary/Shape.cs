using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusClassLibrary
{
    public class Shape : BaseEntity
    {


        public float lat { get; set; }

        public float longit { get; set; }

        public int  sequence { get; set; }


        public Shape(float lat, float longit, int sequence)
        {
            Id = Guid.NewGuid();
            this.lat = lat;
            this.longit = longit;
            this.sequence = sequence;
                
        }
    }
}

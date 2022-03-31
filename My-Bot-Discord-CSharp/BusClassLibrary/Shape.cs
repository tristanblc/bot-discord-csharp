using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusClassLibrary
{
    public class Shape : BaseEntity
    {


        public ulong lat { get; set; }

        public ulong longit { get; set; }

        public int  sequence { get; set; }


        public Shape(ulong _lat, ulong _longit, int _sequence)
        {
            Id = Guid.NewGuid();
            lat = _lat;
            longit = _longit;
            sequence = _sequence;
                
        }
    }
}

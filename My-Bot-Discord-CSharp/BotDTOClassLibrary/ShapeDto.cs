using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDTOClassLibrary
{
    public class ShapeDto : BaseEntity
    {
        public float lat { get; set; }

        public float longit { get; set; }

        public int sequence { get; set; }

    }
}

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
        public ulong lat { get; set; }

        public ulong longit { get; set; }

        public int sequence { get; set; }

    }
}

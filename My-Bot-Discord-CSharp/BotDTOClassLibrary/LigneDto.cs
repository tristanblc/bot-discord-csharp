using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDTOClassLibrary
{
    public class LigneDto : BaseEntity
    {
        public string route_short_name { get; set; }
        public string route_desc { get; set; }

        public string route_type { get; set; }

        public string route_text_color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusClassLibrary
{
    public class Ligne : BaseEntity
    {

        public string route_short_name { get; set; }
        public string route_desc { get; set; }

        public string route_type { get; set; }

        public string route_text_color { get; set; }

        public Ligne(string route_short_name,string route_desc,string route_type,string route_text_color)
        {
            this.route_desc = route_desc;
            this.route_short_name = route_short_name;
            this.route_type = route_type;
            this.route_text_color = route_text_color;   
        }
    }
}

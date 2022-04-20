using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDTOClassLibrary
{
    public class FilmDto
    {
        public string url { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string language { get; set; }

        public string status { get; set; }

        public DateTime premiered { get; set; }

        public DateTime ended { get; set; }

        public string officialSite { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Film
    {
        public string url { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string language { get; set; }

        public string status { get; set; }

        public DateTime premiered {get;set;}

        public DateTime ended { get; set; }

        public  string  officialSite { get; set; }


        public Film(string url, string name, string type,string language,string status,DateTime premiered,DateTime ended, string officialSite)
        {
            this.url = url;
            this.name = name;
            this.type = type;
            this.language = language;
            this.status = status;
            this.ended = ended;
            this.officialSite = officialSite;
            

        }
    }
    
}

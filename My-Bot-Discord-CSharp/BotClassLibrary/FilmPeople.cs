using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Person
    {

        public string url { get;  set; }

        public string name { get; set; }

        public DateTime birthdate { get; set; }

        public DateTime? deathdate { get; set; }

        public Image image { get; set; }

        public Person(string url, string name ,DateTime birthday, DateTime? deathdate,Image image)
        {
            this.url = url;
            this.name = name;
            this.birthdate = birthday;
            this.deathdate = deathdate;
            this.image = image;


        }

    }
}

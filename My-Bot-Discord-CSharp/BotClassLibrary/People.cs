using BotClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public  class People
    {

        public double score { get; set; }
        public Person person { get;  set; }

        public People(double score, Person person)
        {
            this.score = score;
            this.person = person;

        }
    }
}

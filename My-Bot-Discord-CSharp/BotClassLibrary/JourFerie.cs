using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
   public class JourFerie
    {
        public DateTime DateTime { get; set; }

        public string? Name { get; set; }

        public JourFerie(DateTime date, string name)
        {
            Name = name;          
            DateTime = date;    
        }
    }
}

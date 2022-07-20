using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary.Package
{
    public class ReturnMessage :BaseEntity
    {
       

        public string lang { get; private set; }
        public int returnCode { get; private set; }

        public string returnMessage { get; set; }

        public string idShip { get; set; }
        
        public Event? Event { get; set; }

        public ReturnMessage(string lang, int returnCode, string returnMessage, string idShip, Event? evente)
        {
            this.lang = lang;
            this.returnCode = returnCode;
            this.returnMessage = returnMessage;
            this.idShip = idShip;
            this.Event = evente;
        }
    }
}

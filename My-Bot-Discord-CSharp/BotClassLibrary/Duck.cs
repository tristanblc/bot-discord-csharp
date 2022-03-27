using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Duck : Animal
    {

        public  string message { get; init; }

        public Duck(string message,string url) : base(url)
        {
            message = message;
        }
    }
}

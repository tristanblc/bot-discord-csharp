using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Image
    {

        public string medium { get; set; }

        public string original { get; set; }

        public Image(string medium, string original)
        {
            this.medium = medium;
            this.original = original;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary.Package
{
    public class Status : BaseEntity
    {
      
        public DateTime Date { get; private set; }
        public string Label { get; private set; }

        public string Code { get; private set; }

        public Status(DateTime date, string label, string code)
        {
            Date = date;
            Label = label;
            Code = code;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary.Package
{
    public class Event : BaseEntity
    {
       
        public List<Status> Statuses { get; private set; }
        public Event(List<Status> statuses)
        {
            Statuses = statuses;
        }
    }
}

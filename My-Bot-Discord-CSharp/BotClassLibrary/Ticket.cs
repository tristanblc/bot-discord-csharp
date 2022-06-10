using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Ticket : BaseEntity
    {
       

        public string Title { get; init; }
        public string Description { get; init; }

        public DateTime Created { get;init; }

        public string DiscordMember { get; init; }

        public bool IsRead { get; set; }

        public bool IsDeal { get; set; }


        public Ticket(string title, string description, DateTime created, string discordMember)
        {
            Title = title;
            Description = description;
            Created = created;
            DiscordMember = discordMember;
            IsRead = false;
            IsDeal = false;
        }


    }
}

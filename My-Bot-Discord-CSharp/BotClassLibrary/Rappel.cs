using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Rappel : BaseEntity
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string DiscordMember { get; init; }

        public DateTime RappelDate { get; set; }


        public bool IsRead { get; set; }    

        public Rappel(string name, string description, string discordMember, DateTime rappelDate)
        {
            Name = name;
            Description = description;
            DiscordMember = discordMember;
            RappelDate = rappelDate;
            IsRead = false;
        }

    }
}

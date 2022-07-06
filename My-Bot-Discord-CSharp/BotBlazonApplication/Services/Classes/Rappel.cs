
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotBlazonApplication.Services.Classes
{
    public class Rappel : BaseEntity
    {
      

        public string Name { get; init; }

        public string Description { get; init; }

        public string DiscordMember { get; init; }

        public DateTime RappelDate { get; set; }


        public ulong UserDiscordId { get; set; }

        public bool IsRead { get; set; }

        public Rappel(string name, string description, string discordMember, DateTime rappelDate, ulong userDiscordId)
        {
            Name = name;
            Description = description;
            DiscordMember = discordMember;
            RappelDate = rappelDate;
            UserDiscordId = userDiscordId;
            IsRead = false;
        }

    }
}

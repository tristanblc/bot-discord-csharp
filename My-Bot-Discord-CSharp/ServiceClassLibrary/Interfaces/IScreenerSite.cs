using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IScreenerSite
    {
        DiscordEmbedBuilder MakeFileOfSite(string url);
    }
}

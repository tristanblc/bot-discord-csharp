using SteamWebAPI2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    internal interface ISteamGameService
    {
        IDOTA2Match GetIDota(HttpClient httpClient);
        ICSGOServers GetCSGOServer(HttpClient httpClient);

        ISteamEconomy GetISteamEconomy(HttpClient httpClient);

        ITFItems GetITFItems(HttpClient httpClient);
    }
}

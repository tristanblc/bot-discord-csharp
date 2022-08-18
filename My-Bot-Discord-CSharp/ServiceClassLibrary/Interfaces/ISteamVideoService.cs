using SteamWebAPI2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    internal interface ISteamVideoService
    {
        ISteamUser GetISteamUser(HttpClient httpClient);

        string GetReplayURL(string videoId);

        

    }
}

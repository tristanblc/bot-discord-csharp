using DSharpPlus.Entities;
using Steam.Models;
using Steam.Models.SteamCommunity;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    internal interface ISteamService
    {
        SteamWebInterfaceFactory GetClient(string apikey);

        ISteamUser GetISteamUser(HttpClient httpClient);
        List<PlayerSummaryModel> GetListsSteamUser(ulong steamUserId);
        List<FriendModel> GetFriendsListBySteamUserId(ulong steamUserId);
        List<PlayerBansModel> GetBansSteamUser(ulong steamUserId);


        DiscordEmbedBuilder ConvertSteamUsetToEmbed(SteamUser steamUser);


        ISteamApps GetISteamApps(HttpClient httpClient);

        List<SteamAppModel> GetSteamAppsByName(string name);
        List<SteamAppModel> GetSteamApps();

        List<SteamAppModel> GetSteamAppsByDateAsc(string date);

        List<SteamAppModel> GetSteamAppsByDateDesc(string date);
        DiscordEmbedBuilder ConvertSteamAppToEmbed(SteamAppModel steamAppModel);
        

    }
}

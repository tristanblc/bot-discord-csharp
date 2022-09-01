using DSharpPlus.Entities;
using Steam.Models;
using Steam.Models.CSGO;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamEconomy;
using Steam.Models.TF2;
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
        ICSGOServers GetCSGOServer(HttpClient httpClient);

        ISteamEconomy GetISteamEconomy(HttpClient httpClient);

        ITFItems GetITFitems(HttpClient httpClient);

        ISteamApps GetISteamApps(HttpClient httpClient);
        ISteamWebAPIUtil GetISteamWebAPIUtil(HttpClient httpClient);

        ISteamRemoteStorage GetISteamRemoteStorage(HttpClient httpClient);

        IPlayerService GetIPlayerService(HttpClient httpClient);

        bool IsPlayingSharedGameAsync(ulong steamId, string appId);

        string GetUserSteamPage(string steamId);
        List<BadgeModel> GetUserBagdes(string steamId);

        ServerStatusModel GetServerStatus();

        AssetClassInfoResultModel GetAssetInfo(string appname);

        AssetPriceResultModel GetPriceAssset(string appname, string currency);

        List<GoldenWrenchModel> GetGoldenWrenchModels();
        SteamServerInfoModel GetServerApiInfo();

        List<SteamInterfaceModel> GetSuppportedApiList();


        List<PublishedFileDetailsModel> GetPublishedFileDetails(List<ulong> steamRemoteIds);

        DiscordEmbedBuilder ConvertServerStatusToEmbed(SteamServerInfoModel steamServerModel);

        DiscordEmbedBuilder ConvertSteamInterfaceToEmbed(SteamInterfaceModel steamInterfaceModel);

        DiscordEmbedBuilder ConvertServerStatusToEmbed(ServerStatusModel serverStatus);

        DiscordEmbedBuilder ConvertAssetClassToEmbed(AssetClassInfoResultModel asset);

        DiscordEmbedBuilder ConvertGoldenWrenchToEmbed(GoldenWrenchModel goldenWrench);
        DiscordEmbedBuilder CreateUserPageEmbed(string steamId);
     
    }
}

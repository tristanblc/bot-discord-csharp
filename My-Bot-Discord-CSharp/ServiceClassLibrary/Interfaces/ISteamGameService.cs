using DSharpPlus.Entities;
using Steam.Models.CSGO;
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

        ServerStatusModel GetServerStatus();

        AssetClassInfoResultModel GetAssetInfo(string appname);

        AssetPriceResultModel GetPriceAssset(string appname, string currency);

        List<GoldenWrenchModel> GetGoldenWrenchModels();

        DiscordEmbedBuilder ConvertServerStatusToEmbed(ServerStatusModel serverStatus);

        DiscordEmbedBuilder ConvertAssetClassToEmbed(AssetClassInfoResultModel asset);

        DiscordEmbedBuilder ConvertAssetPriceResultToEmbed(AssetPriceResultModel asset);
    }
}

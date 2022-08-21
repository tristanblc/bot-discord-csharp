using DSharpPlus.Entities;
using Steam.Models;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamPlayer;
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


        DiscordEmbedBuilder ConvertFriendModelToEmbed(FriendModel friendModel);

        List<PlayerBansModel> GetBansSteamUser(ulong steamUserId);
        
        DiscordEmbedBuilder ConvertPlayerBans(PlayerBansModel playerBans);

        DiscordEmbedBuilder ConvertSteamUsetToEmbed(PlayerSummaryModel player);


        ISteamApps GetISteamApps(HttpClient httpClient);

        List<SteamAppModel> GetSteamAppsByName(string name);
        List<SteamAppModel> GetSteamApps();
        List<SteamAppModel> GetSteamAppsById(ulong appId);

        List<SteamAppModel> GetSteamAppsByDateAsc(string date);

        List<SteamAppModel> GetSteamAppsByDateDesc(string date);
        DiscordEmbedBuilder ConvertSteamAppToEmbed(SteamAppModel steamAppModel);

        ISteamNews GetISteamNews(HttpClient httpClient);

        SteamNewsResultModel GetSteamNewsForApp(uint appId);

        DiscordEmbedBuilder ConvertSteamNewsToEmbed(SteamNewsResultModel SteamNewsModel);

        ISteamUserStats GetISteamUserStats(HttpClient httpClient);

        UserStatsForGameResultModel GetUserStatsForGame(string gameName,uint userId);

        SchemaForGameResultModel GetSchemaForGame(string gameName);

        uint GetCurrentUserOnlineForGame(string gameName);

        List<GlobalStatModel> GetGlobalGameStats(string gameName, DateTime startDate, DateTime? endDatetime);

        List<PlayerAchievementModel> GetUserAchievementForUser(string gameName, uint userId);

        DiscordEmbedBuilder ConvertPlayerAchivementsModelToEmbed(PlayerAchievementModel achievementModel);

        DiscordEmbedBuilder ConvertGlobalStatModelToEmbed(GlobalStatModel globalStatModel);

        DiscordEmbedBuilder ConvertUserStatsForGameToeEmbed(UserStatsForGameResultModel userStatsModel);

        DiscordEmbedBuilder ConvertSchemaGameModeToEmbed(SchemaForGameResultModel schemaModel);

    }
}

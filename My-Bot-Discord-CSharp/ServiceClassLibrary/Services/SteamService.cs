using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using Steam.Models;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamPlayer;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class SteamService : ISteamService
    {

        private SteamWebInterfaceFactory SteamWebInterface { get; init; }

        private ILoggerProject LoggerProject { get; init; }

        private IUtilsService UtilsService { get; init; }
        private HttpClient HttpClient { get; init; }
        private ISteamUser SteamUser { get; init; }
        private ISteamApps SteamApps { get; init; }
        private ISteamNews SteamNews { get; init; }

        private ISteamUserStats SteamUserStats { get; init; }

        public SteamService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
            SteamApps = this.GetISteamApps(HttpClient);
            SteamUser = this.GetISteamUser(HttpClient);
            SteamNews = this.GetISteamNews(HttpClient);
            SteamUserStats = this.GetISteamUserStats(HttpClient);


        }
        public SteamWebInterfaceFactory GetClient(string apikey)
        {
            try
            {
                return new SteamWebInterfaceFactory(apikey);

            }
            catch (Exception ex)
            {

                var message = "Cannot get construct client";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }

        }

        public ISteamUser GetISteamUser(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamUser>(new HttpClient());
            }
            catch (Exception ex)
            {
                var message = "Cannot get interface Steam User";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }
        }

        public List<PlayerSummaryModel> GetListsSteamUser(List<ulong> SteamUserIds)
        {
            try
            {
                return SteamUser.GetPlayerSummariesAsync(SteamUserIds).Result.Data.ToList();
            }
            catch (Exception ex)
            {
                var message = "Cannot get lists Steam User";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }
        }

        public List<PlayerSummaryModel> GetListsSteamUser(ulong SteamUserId)
        {
            try
            {
                return SteamUser.GetPlayerSummariesAsync(new List<ulong>() { SteamUserId }).Result.Data.ToList();

            }
            catch (Exception ex)
            {
                var message = "Cannot get e Steam User";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public List<FriendModel> GetFriendsListBySteamUserId(ulong steamUserId)
        {
            try
            {
                return SteamUser.GetFriendsListAsync(steamUserId).Result.Data.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get friends user from user  with id {steamUserId.ToString()}";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public List<PlayerBansModel> GetBansSteamUser(ulong steamUserId)
        {
            try
            {
                return SteamUser.GetPlayerBansAsync(steamUserId).Result.Data.ToList();
            }
            catch (Exception ex)
            {

                var message = $"Cannot get ban user from user  with id {steamUserId.ToString()}";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public DiscordEmbedBuilder ConvertSteamUsetToEmbed(PlayerSummaryModel player)
        {
            try
            {
                var contents = $"Nickname : {player.Nickname} ";
                contents += $"\nSteam id : {player.SteamId}";
                contents += $"\nStatus : {player.UserStatus}";
                if (player.PlayingGameId != null)
                {
                    var games = this.GetSteamAppsById(ulong.Parse(player.PlayingGameId));
                    games.ForEach(game =>
                    {
                        contents += $"\nGame : {game.Name}";
                    });
                }
                var embed = UtilsService.CreateNewEmbed($"{player.Nickname}", DiscordColor.Purple, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var message = $"Cannot get steam apps interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public ISteamApps GetISteamApps(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamApps>(httpClient);

            }
            catch (Exception ex)
            {
                var message = $"Cannot get steam apps interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public List<SteamAppModel> GetSteamAppByName(string name)
        {
            try
            {
                var list = SteamApps.GetAppListAsync().Result.Data.Where(x => x.Name == name).ToList();
                var apps = new List<SteamAppModel>();

                list.ToList().ForEach(app =>
                {
                    if (app.Name.ToLower() != name.ToLower())
                    {
                        list.Remove(app);

                    }

                });

                return list;

            }
            catch (Exception ex)
            {
                var message = $"Cannot get owned games";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public List<SteamAppModel> GetSteamApps()
        {
            try
            {
                return SteamApps.GetAppListAsync().Result.Data.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get owned games";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public DiscordEmbedBuilder ConvertSteamAppToEmbed(SteamAppModel steamAppModel)
        {
            try
            {
                var contents = $"{steamAppModel.AppId} - {steamAppModel.Name}";
                contents += $"Link : https://store.steampowered.com/app/{steamAppModel.AppId}/{steamAppModel.Name}/";
                var embed = UtilsService.CreateNewEmbed($"{steamAppModel.Name}", DiscordColor.Purple, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var message = $"Cannot convert to embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public List<SteamAppModel> GetSteamAppsByName(string name)
        {

            try
            {
                return SteamApps.GetAppListAsync().Result.Data.ToList().OrderByDescending(x => x.AppId).ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get lists apps games desc";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public List<SteamAppModel> GetSteamAppsByDateAsc(string date)
        {
            try
            {
                return SteamApps.GetAppListAsync().Result.Data.ToList().OrderBy(x => x.AppId).ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get lists apps games asc";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public List<SteamAppModel> GetSteamAppsByDateDesc(string date)
        {

            try
            {
                return SteamApps.GetAppListAsync().Result.Data.ToList().OrderByDescending(x => x.AppId).ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get lists apps games asc";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public DiscordEmbedBuilder ConvertFriendModelToEmbed(FriendModel friendModel)
        {
            try
            {
                var friend = this.GetListsSteamUser(friendModel.SteamId).First();



                var contents = $"Steam id : {friendModel.SteamId}";
                contents += $"\nRelationship : {friend.Nickname}";
                contents += $"\nDate:{friendModel.FriendSince.ToString()}";
                contents += $"\nStatus: {friend.UserStatus}";
                if (friend.PlayingGameId != null)
                {
                    var games = this.GetSteamAppsById(ulong.Parse(friend.PlayingGameId));
                    games.ForEach(game =>
                    {
                        contents += $"\nGame : {game.Name}";
                    });
                }

                var embed = UtilsService.CreateNewEmbed($"Friend named {friendModel.SteamId}", DiscordColor.Aquamarine, contents);
                return embed;


            }
            catch (Exception ex)
            {
                var message = $"Cannot convert friendmodel to embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public DiscordEmbedBuilder ConvertPlayerBans(PlayerBansModel playerBans)
        {
            try
            {
                var embed = UtilsService.CreateNewEmbed($"Is vac?", DiscordColor.Aquamarine, "No");
                if (playerBans.VACBanned == true)
                {
                    var contents = $"Yes. Why?";
                    contents += $"\nLast Ban Date {playerBans.DaysSinceLastBan}";
                    contents += $"\nNumber of bans games : {playerBans.NumberOfGameBans}";
                    contents += $"\nIs community bans? : {playerBans.CommunityBanned}";
                    embed.Description = contents;
                }

                return embed;

            }
            catch (Exception ex)
            {
                var message = $"Cannot convert friendmodel to embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
            throw new NotImplementedException();
        }

        public List<SteamAppModel> GetSteamAppsById(ulong appId)
        {
            try
            {
                return SteamApps.GetAppListAsync().Result.Data.ToList().Where(x => x.AppId == appId).ToList();
            }
            catch (Exception ex)
            {
                var message = $"Cannot get lists apps games asc";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public SteamNewsResultModel GetSteamNewsForApp(uint appId)
        {
            try
            {
                return SteamNews.GetNewsForAppAsync(appId).Result.Data;
            }
            catch (Exception ex)
            {
                var message = $"Cannot get lists of news from steam";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }


        }

        public DiscordEmbedBuilder ConvertSteamNewsToEmbed(SteamNewsResultModel SteamNewsModel)
        {
            try
            {
                var contents = $"AppId : {SteamNewsModel.AppId}";
                SteamNewsModel.NewsItems.ToList().ForEach(news =>
                {
                    contents += $"Author : {news.Author}";
                    contents += $"Date :{news.Date}";
                    contents += $"\nFeed Name : {news.Feedname}";
                    contents += $"\nNumber of tags :{news.Tags.Length}";
                    contents += $"{news.Contents}";

                });
                var embed = UtilsService.CreateNewEmbed($"News", DiscordColor.Aquamarine, contents);
                return embed;

            }
            catch (Exception ex)
            {
                var message = $"Cannot create embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public ISteamNews GetISteamNews(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamNews>(new HttpClient());
            }
            catch(Exception ex)
            {
                var message = $"Cannot steam news interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
            
        }



        public UserStatsForGameResultModel GetUserStatsForGame(string gameName, uint userId)
        {
            try
            {
                var game = this.GetSteamAppByName(gameName).First();
                return SteamUserStats.GetUserStatsForGameAsync(userId, game.AppId).Result.Data;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user stats for game {gameName}";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }

        }

        public SchemaForGameResultModel GetSchemaForGame(string gameName)
        {
            try
            {
                var language = "english";
                var game = this.GetSteamAppByName(gameName).First();
                return SteamUserStats.GetSchemaForGameAsync(game.AppId, language).Result.Data;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get schema for game {gameName}";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            };
        }

        public uint GetCurrentUserOnlineForGame(string gameName)
        {
            try
            {

                var game = this.GetSteamAppByName(gameName).First();
                return SteamUserStats.GetNumberOfCurrentPlayersForGameAsync(game.AppId).Result.Data;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot get current user online";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);

            }
        }

        public List<GlobalStatModel> GetGlobalGameStats(string gameName, DateTime startDate, DateTime? endDatetime)
        {
            try
            {
                var game = this.GetSteamAppsByName(gameName).First();
                var stats = SteamUserStats.GetGlobalStatsForGameAsync(game.AppId, new List<string>() { "count", "name" }, startDate, endDatetime).Result.Data.ToList();
                return stats;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot get game  {gameName} global stats";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);

            }
        }

        public List<PlayerAchievementModel> GetUserAchievementForUser(string gameName, uint userId)
        {
            try
            {
                var game = this.GetSteamAppByName(gameName).First();
                return SteamUserStats.GetPlayerAchievementsAsync(game.AppId, userId, "english").Result.Data.Achievements.ToList();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user achievement for user for game {gameName}";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }
        }

        public ISteamUserStats GetISteamUserStats(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamUserStats>();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get interface IsteamUserStats";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);

            }

        }

        public DiscordEmbedBuilder ConvertPlayerAchivementsModelToEmbed(PlayerAchievementModel achievementModel)
        {
            try
            {
                var contents = $" when : {achievementModel.UnlockTime.ToLocalTime()}";
                contents += $"\nDescription: {achievementModel.Description}";
                if (achievementModel.Achieved != 0)
                    contents = $" Achivement lock : Unlock";

                if(achievementModel.Achieved != 0) 
                  contents = $" Achivement lock : Unlock";

                else
                {
                    contents = $" Achivement lock : lock";
                }
                var embed = UtilsService.CreateNewEmbed($"Name {achievementModel.Name}", DiscordColor.Aquamarine, contents);
                return embed;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot get interface IsteamUserStats";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);

            }

        }

        public DiscordEmbedBuilder ConvertGlobalStatModelToEmbed(GlobalStatModel globalStatModel)
        {
            try
            {
                var contents = $"{globalStatModel.Name}";
                contents += $"\nTotal: {globalStatModel.Total}";
                var embed = UtilsService.CreateNewEmbed($"Stats for app {globalStatModel.Name}", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get embed";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }


        }

        public DiscordEmbedBuilder ConvertUserStatsForGameToeEmbed(UserStatsForGameResultModel userStatsModel)
        {
            try
            {
                var contents = $"{userStatsModel.GameName}";
                userStatsModel.Stats.ToList().ForEach(stat =>
                {
                    contents += $"\nStat Name :{stat.Name}  - Value :{stat.Value}";
                });
                var embed = UtilsService.CreateNewEmbed($" User Stats for app {userStatsModel.GameName}", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch (Exception)
            {
                var exception = $"Cannot get embed";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }

        }

        public DiscordEmbedBuilder ConvertSchemaGameModeToEmbed(SchemaForGameResultModel schemaModel)
        {
            try
            {
                var contents = $"{schemaModel.GameName}";
                contents += "Game achievement :";
                schemaModel.AvailableGameStats.Achievements.ToList().ForEach(achievemement =>
                {
                    contents += $"\nName : {achievemement.DisplayName} -  Default value : {achievemement.DefaultValue}";

                });

                contents += $"\n Game version : {schemaModel.GameVersion}";
                var embed = UtilsService.CreateNewEmbed($" Game {schemaModel.GameName} schema", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get embed";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }

        }

        public SteamAppUpToDateCheckModel GetUpToDateCheckModelByVersionAndAppId(string appId, uint version)
        {
            try
            {
                return SteamApps.UpToDateCheckAsync(uint.Parse(appId), version).Result.Data;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot check if is outdated or updated version";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);
            }
        }

        public DiscordEmbedBuilder ConvertUpToDateAppToEmbed(SteamAppUpToDateCheckModel app)
        {
            try
            {
                var contents = $"";
                contents += $"\n {app.Message}";
                if(app.UpToDate)
                    contents += $"\nIs up to date ";
                else
                    contents += $"\n Is outdated -> app is update to {app.RequiredVersion}";
                var embed = UtilsService.CreateNewEmbed($"App check version", DiscordColor.Aquamarine, contents);
                return embed;

            }
            catch(Exception ex)
            {
                var exception = $"Cannot convert to embed";
                LoggerProject.WriteLogErrorLog(exception);
                throw new SteamException(exception);

            }
         
        }
    }

}

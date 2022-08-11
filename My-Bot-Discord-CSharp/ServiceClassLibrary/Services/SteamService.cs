using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using Steam.Models;
using Steam.Models.SteamCommunity;
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
            
        public SteamService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
            SteamApps = this.GetISteamApps(HttpClient);
            SteamUser = this.GetISteamUser(HttpClient);
        }
        public SteamWebInterfaceFactory GetClient(string apikey)
        {
            try
            {
                return new SteamWebInterfaceFactory(apikey);
          
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                var embed = UtilsService.CreateNewEmbed($"{player.Nickname}", DiscordColor.Purple,contents);
                return embed;
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
                return SteamApps.GetAppListAsync().Result.Data.ToList().TakeWhile(x => x.Name == name).ToList();

            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex) {
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
    }
}

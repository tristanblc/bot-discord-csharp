using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Analytics;
using TwitchLib.Api.Helix.Models.Games;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace ServiceClassLibrary.Services
{
    public class TwitchService : ITwitchService
    {
        private TwitchAPI TwitchClient { get; init; }

        private ILoggerProject Logger { get; init; }

        private IUtilsService UtilsService { get; init; }


        public TwitchService(string accesstoken, string clientId)
        {

            Logger = new LoggerProject();
            UtilsService = new UtilsService();
            TwitchClient = this.ConnectToTwitch(clientId, accesstoken);

        }

        public TwitchAPI ConnectToTwitch(string clientid, string accessToken)
        {
            try
            {
                var client = new TwitchAPI();
                client.Settings.AccessToken = accessToken;
                client.Settings.ClientId = clientid;
                return client;


            }
            catch (Exception ex) {
                var exception = $"Cannot connect to twitch service  with client id {clientid} & access token {accessToken}";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);

            }
        }


        public User GetUserById(string id)
        {
            try
            {
                return TwitchClient.Helix.Users.GetUsersAsync(new List<string>() { id }, null, null).Result.Users.ToList().First();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public List<Follow> GetFollowedUser(string id)
        {
            try
            {
                return TwitchClient.Helix.Users.GetUsersFollowsAsync(null, null, 20, id, null).Result.Follows.ToList();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get follewed users";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public List<TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream> GetStreams(string username)
        {
            try
            {
               

                return TwitchClient.Helix.Streams.GetStreamsAsync(null, null, 20, null, null, "all", new List<string>() { username}, null).Result.Streams.ToList();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get stream";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream GetStreamById(string broadcasterId)
        {
            try
            {
                return this.GetStreams(broadcasterId).First();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get stream by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public DiscordEmbedBuilder ConvertTwitchUserToEmbed(User user)
        {
            try
            {
                var contents = $"Email: {user.Email}";
                contents += $"Name {user.DisplayName}";
                contents += $"Broadcast : {user.BroadcasterType.ToLower()}";
                contents += $"{user.Description}";

                var embed = UtilsService.CreateNewEmbed($"{user.DisplayName}", DiscordColor.Azure, contents);
                return embed;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot to convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public DiscordEmbedBuilder ConvertFollowersTwitchToEmbed(Follow follower)
        {
            try
            {
                var contents = $"Username {follower.FromUserName}";
                contents += $"{follower.FollowedAt.ToLocalTime().ToString()}";
                var embed = UtilsService.CreateNewEmbed($"{follower.FromUserName}", DiscordColor.Azure, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot to convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public DiscordEmbedBuilder ConvertStreamTwitchToEmbed(TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream stream)
        {
            try
            {
                var contents = $"Informations : ";
                contents += $"{stream.UserName} - Start At {stream.StartedAt} -> play {stream.GameName} : + {stream.Language}";
                if (stream.IsMature)
                    contents += $"\n Not allow for -18";
                contents += $"\nNumber of viewer {stream.ViewerCount.ToString()}";
                contents += $"\n{stream.ThumbnailUrl}";
                contents += $"thx to watching this <3";
                var embed = UtilsService.CreateNewEmbed($"{stream.UserName} plays {stream.GameName}", DiscordColor.Azure, contents);
                return embed;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot to convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public List<GameAnalytics> GetAnalyticsForGame(string gamename)
        {
            try
            {
                var game = this.GetGameFromName(gamename);
                return TwitchClient.Helix.Analytics.GetGameAnalyticsAsync(game.Id).Result.Data.ToList();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot to convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }

        }

        public Game GetGameFromName(string name)
        {
            try
            {
                return this.GetGames(new List<string>() { name }).First();

            }
            catch (Exception ex)
            {
                var exception = $"Cannot to convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public List<Game> GetGames(List<string> gamesNames)
        {
            try
            {
                return TwitchClient.Helix.Games.GetGamesAsync(null, gamesNames).Result.Games.ToList();
            }
            catch(Exception ex)
            {
                var exception = $"Cannot get games";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
          
        }

        public DiscordEmbedBuilder ConvertGameStatTwitchToEmbed(GameAnalytics gameAnalytics)
        {
            try
            {
                var contents = $"{gameAnalytics.GameId}";
                contents += $"{gameAnalytics.DateRange.StartedAt} - ended at {gameAnalytics.DateRange.EndedAt}";
                contents += $"{gameAnalytics.Url}";

                var embed = UtilsService.CreateNewEmbed($"Game analysis from game id {gameAnalytics.GameId}", DiscordColor.Azure, contents);
                return embed;

            }
            catch(Exception ex)
            {
                var exception = $"Cannot convert to discord embed";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

       
    }
}

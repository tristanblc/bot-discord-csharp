using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Helix.Models.Analytics;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Chat.Badges;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;

using TwitchLib.Api.Helix.Models.Clips.GetClips;
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

        private IVideoService VideoService { get; init; }

        private IWebClientDownloader WebClientDownloader { get; init; }
        private string DirectoryForSave { get; init; } = Directory.GetCurrentDirectory();

        private WebClient WebClient { get; init; }
        public TwitchService(string accesstoken, string clientId)
        {

            Logger = new LoggerProject();
            UtilsService = new UtilsService();
            TwitchClient = this.ConnectToTwitch(clientId, accesstoken);
            WebClient = new WebClient();
            WebClientDownloader = new WebClientDownloader(WebClient);
            VideoService = new VideoService();
        }

        public TwitchAPI ConnectToTwitch(string clientid, string accessToken)
        {
            try
            {
                var client = new TwitchAPI();
                client.Settings.ClientId = clientid;
                client.Settings.Secret = accessToken;
                client.Settings.AccessToken = client.Auth.GetAccessTokenAsync(null).Result;
         
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

        public List<TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream> GetStreams(string broadcasterId)
        {
            try
            {
                return TwitchClient.Helix.Streams.GetStreamsAsync(null, null, 20, null, null, "all", new List<string>() { broadcasterId }, null, null).Result.Streams.ToList();
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
                var contents = $"Username {follower.ToUserName}";
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


        public User GetUserByName(string name)
        {
            try
            {
                return TwitchClient.Helix.Users.GetUsersAsync(null, new List<string>() { name }, null).Result.Users.ToList().First();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public List<Clip> Get10LatestClipsFromUser(string username)
        {
            try
            {
                var user = GetUserByName(username);
                return TwitchClient.Helix.Clips.GetClipsAsync(null, null, user.Id, null, null, DateTime.Now.AddDays(-1), DateTime.Now, 10, null).Result.Clips.ToList();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }


        public DiscordEmbedBuilder ConvertTwitchClipToEmbed(Clip clip)
        {
            try
            {

                var contents = $"User name : {clip.CreatorName} - Duration : {clip.Duration}";
                contents += $"\n{clip.Url}";
                var embed = UtilsService.CreateNewEmbed($"Clip from {clip.CreatorName}", DiscordColor.Azure, contents);
                embed.WithImageUrl(clip.ThumbnailUrl);
                return embed;

            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public Clip GetLatestClipByUsername(string username)
        {
            try
            {
                var user = GetUserByName(username);
                return TwitchClient.Helix.Clips.GetClipsAsync(null, null, user.Id, null, null, DateTime.Now.AddDays(-1), DateTime.Now, 1, null).Result.Clips.ToList().First();
            }
            catch (Exception ex)
            {
                var exception = $"Cannot get user by id";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public DiscordEmbedBuilder ConvertGameToEmbed(Game game)
        {
            try
            {
                var contents = $"Game {game.Name}";
                contents += $"\nId {game.Id}";
                var embed = UtilsService.CreateNewEmbed($"Game info", DiscordColor.Azure, contents);
                embed.WithImageUrl(game.BoxArtUrl);
                return embed;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot convert game to embed;";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }


        public List<ChannelInformation> GetChannelsinformation(string username)
        {
            try
            {
                var user = GetUserByName(username);
                return TwitchClient.Helix.Channels.GetChannelInformationAsync(user.Id, null).Result.Data.ToList();

            }
            catch (Exception ex)
            {
                var exception = $"Cannot convert game to embed;";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
       
        }


      
 

        public DiscordEmbedBuilder ConvertChannelsToEmbed(ChannelInformation channelInfo)
        {
            try
            {
                var contents = $"Game: {channelInfo.GameName}";
                contents = $"\nName: {channelInfo.BroadcasterName}";           
                contents = $"\nLanguage: {channelInfo.BroadcasterLanguage}";
                contents = $"\nTitle: {channelInfo.Title}";
                contents = $"\nDelay: {channelInfo.Delay}";
                var embed = UtilsService.CreateNewEmbed($"Game info", DiscordColor.Azure, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var exception = $"Cannot convert  channel info to embed;";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
       
        }

        public void ConvertEmojiToEmbed(string broadcasterId, CommandContext ctx)
        {
            try
            {

            
                var emojis = getEmojisFromBroadcasterId(broadcasterId);
                Task.Delay(2000);
                emojis.ChannelEmotes.ToList().ForEach(emoj =>
                {
                        var contents = $"Emoji info";
                   
                        contents += $"\nName  : {emoj.Name}";                  
                        contents += $"\nTier : {emoj.Tier}";
                        contents += $"\nUrl x1  : {emoj.Images.Url1X}";
                        contents += $"\nUrl x2 : {emoj.Images.Url2X}";
                        contents += $"\nUrl x4 : {emoj.Images.Url4X}";
                        var embed = UtilsService.CreateNewEmbed($"Emoji info {emoj.Name}", DiscordColor.Azure, contents);
                        embed.WithThumbnail(emoj.Images.Url1X);
                        embed.WithImageUrl(emoj.Images.Url2X);
                        ctx.RespondAsync(embed.Build());
                        Task.Delay(2000);
                        contents = "";


                });
                
           
            }
            catch (Exception ex)
            {
                var exception = $"Cannot convert  channel info to embed;";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
        }

        public GetChannelEmotesResponse getEmojisFromBroadcasterId(string broadcasterId)
        {
      
            try
            {
                return TwitchClient.Helix.Chat.GetChannelEmotesAsync(broadcasterId).Result;
                
            
             }
            catch(Exception ex)
            {
                var exception = $"Cannot convert emoji";
                Logger.WriteLogErrorLog(exception);
                throw new TwitchAPIException(exception);
            }
         
        }


    }
}

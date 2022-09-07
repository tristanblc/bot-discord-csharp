using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Core.Models.Undocumented.CSStreams;

using TwitchLib.Api.Helix;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Chat.Badges;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;

using TwitchLib.Api.Helix.Models.Clips.GetClips;
using TwitchLib.Api.Helix.Models.Users;

namespace ServiceClassLibrary.Interfaces
{
    public interface ITwitchService
    {
        TwitchAPI ConnectToTwitch(string clientid, string accessToken);

        TwitchLib.Api.Helix.Models.Users.GetUsers.User GetUserById(string id);

        List<TwitchLib.Api.Helix.Models.Users.GetUserFollows.Follow> GetFollowedUser(string id);

        TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream GetStreamById(string broadcasterId);

        List<TwitchLib.Api.Helix.Models.Analytics.GameAnalytics> GetAnalyticsForGame(string gamename);
       
        TwitchLib.Api.Helix.Models.Games.Game GetGameFromName(string name);


        List<TwitchLib.Api.Helix.Models.Games.Game> GetGames(List<string> gamesNames);

        Clip GetLatestClipByUsername(string username);

        List<Clip> Get10LatestClipsFromUser(string username);


        List<ChannelInformation> GetChannelsinformation(string username);

        GetChannelEmotesResponse getEmojisFromBroadcasterId(string broadcasterId);


        DiscordEmbedBuilder ConvertTwitchClipToEmbed(Clip clip);


        void ConvertEmojiToEmbed(string broadcasterId,CommandContext ctx);



        void DownloadLatestVideo(string broadcasterName);

        DiscordEmbedBuilder ConvertTwitchUserToEmbed(TwitchLib.Api.Helix.Models.Users.GetUsers.User user);
        DiscordEmbedBuilder ConvertFollowersTwitchToEmbed(TwitchLib.Api.Helix.Models.Users.GetUserFollows.Follow follower);

        DiscordEmbedBuilder ConvertStreamTwitchToEmbed(TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream stream);

        DiscordEmbedBuilder ConvertGameStatTwitchToEmbed(TwitchLib.Api.Helix.Models.Analytics.GameAnalytics gameAnalytics);

        DiscordEmbedBuilder ConvertGameToEmbed(TwitchLib.Api.Helix.Models.Games.Game game);
    }
}

using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Core.Models.Undocumented.CSStreams;
using TwitchLib.Api.Helix.Models.Users;

namespace ServiceClassLibrary.Interfaces
{
    public interface ITwitchService
    {
        TwitchAPI ConnectToTwitch(string clientid, string accessToken);

        TwitchLib.Api.Helix.Models.Users.GetUsers.User GetUserById(string id);

        List<TwitchLib.Api.Helix.Models.Users.GetUserFollows.Follow> GetFollowedUser(string id);

        List<TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream> GetStreams(string broadcasterId);

        TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream GetStreamById(string broadcasterId);

        DiscordEmbedBuilder ConvertTwitchUserToEmbed(TwitchLib.Api.Helix.Models.Users.GetUsers.User user);
        DiscordEmbedBuilder ConvertFollowersTwitchToEmbed(TwitchLib.Api.Helix.Models.Users.GetUserFollows.Follow follower);

        DiscordEmbedBuilder ConvertStreamTwitchToEmbed(TwitchLib.Api.Helix.Models.Streams.GetStreams.Stream stream);

    }
}

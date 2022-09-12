using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Models;
using Tweetinvi.Models;
using TwitchLib.Api.Helix;

namespace ServiceClassLibrary.Interfaces
{
    internal interface ITwitterService
    {
        IUser GetUsers(string username);
        IAuthenticatedUser GetAuthenticatedUser();
        ITweet GetTweetById(int id);
        long[] GetLongsFriendsLists(string username);
        List<IUser> GetFriendsLists(string username);
        long[] GetLongsFollowers(string username);
        List<IUser> GetFollowersUsers(string username);

        DiscordEmbedBuilder ConvertIUserToEmbed(IUser user);

        DiscordEmbedBuilder ConvertITweetToEmbed(ITweet tweet);

    }
}

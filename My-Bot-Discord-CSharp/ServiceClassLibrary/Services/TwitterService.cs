using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Models;
using Tweetinvi.Models;
using TwitchLib.Api.Helix;

namespace ServiceClassLibrary.Services
{
   public  class TwitterService : ITwitterService
    {
        private TwitterClient TwitterClient { get; init; }
        private IUtilsService UtilsService { get; init; }  
        private ILoggerProject Logger { get; init; }

        public TwitterService(string consumerKey, string consumerSecret)
        {
            TwitterClient = new TwitterClient(consumerKey, consumerSecret);
            UtilsService = new UtilsService();
            Logger = new LoggerProject();
        }

        public IUser GetUsers(string username)
        {
            try
            {
                return TwitterClient.Users.GetUserAsync(username).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
           
        }

        public IAuthenticatedUser GetAuthenticatedUser()
        {
            try
            {
                return TwitterClient.Users.GetAuthenticatedUserAsync().Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

        public ITweet GetTweetById(int id)
        {
            try
            {
                return TwitterClient.Tweets.GetTweetAsync(id).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

        public long[] GetLongsFriendsLists(string username)
        {
            try
            {
                return TwitterClient.Users.GetFriendIdsAsync(username).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }
        public List<IUser> GetFriendsLists(string username)
        {
            try
            {
                var userLong = GetLongsFriendsLists(username).ToList();
                return TwitterClient.Users.GetUsersAsync(userLong).Result.ToList();
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }


        public long[] GetLongsFollowers(string username)
        {
            try
            {
                return TwitterClient.Users.GetFollowerIdsAsync(username).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

        public List<IUser> GetFollowersUsers(string username)
        {
            try
            {
                var userLong = GetLongsFriendsLists(username).ToList();
                return TwitterClient.Users.GetUsersAsync(userLong).Result.ToList();
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

    }
}

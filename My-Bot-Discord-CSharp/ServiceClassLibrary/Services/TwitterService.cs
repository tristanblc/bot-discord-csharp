using DSharpPlus.Entities;
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
using Tweetinvi.Models.V2;
using TwitchLib.Api.Helix;

namespace ServiceClassLibrary.Services
{
   public  class TwitterService : ITwitterService
    {
        private TwitterClient TwitterClient { get; init; }
        private IUtilsService UtilsService { get; init; }  
        private ILoggerProject Logger { get; init; }

        public TwitterService(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            TwitterClient = new TwitterClient(apiKey, apiSecret, accessToken, accessSecret);
            TwitterClient.Auth.RequestAuthenticationUrlAsync();
            UtilsService = new UtilsService();
            Logger = new LoggerProject();
           
        }

        public UserV2Response GetUsers(string username)
        {
            try
            {
                return TwitterClient.UsersV2.GetUserByNameAsync(username).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
           
        }

        public TweetV2Response GetTweetByName(string name)
        {
            try
            {
                return TwitterClient.TweetsV2.GetTweetAsync(name).Result;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }


        public DiscordEmbedBuilder ConvertUserToEmbed(UserV2Response user)
        {
            try
            {
                var contents = $"User id : {user.User.Id}";
                contents += $"\nName : {user.User.Name}";
                contents += $"\nUsername :{user.User.Username}";
                contents += $"Description : {user.User.Description}";
                contents += $"\nIs verified : {user.User.Verified}";
                var embed = UtilsService.CreateNewEmbed($"Twitter user info", DiscordColor.Brown,contents);
                embed.WithUrl(user.User.ProfileImageUrl);
                return embed;
            }
            catch (Exception)
            {
                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

        public DiscordEmbedBuilder ConvertTweetToEmbed(TweetV2 tweet)
        {
            var contents = $"\"";

            contents += $"\nContent : {tweet.Text}";
            contents += $"\nRetweet count : {tweet.PublicMetrics.RetweetCount}";
            contents += $"\nLike count : {tweet.PublicMetrics.LikeCount}";
            contents += $"\nQuote count : {tweet.PublicMetrics.QuoteCount}";
            var embed = UtilsService.CreateNewEmbed($"Twitter user info", DiscordColor.Brown, contents);      
            return embed;
        }

        public List<TweetV2> GetTweetsByName(string name)
        {
            try
            {
                var searchIterator = TwitterClient.SearchV2.GetSearchTweetsV2Iterator(name);
                List<TweetV2> listTweets = new List<TweetV2>();
                while (!searchIterator.Completed)
                {
                    var searchPage = searchIterator.NextPageAsync().Result;
                    int i = 0;
                    var searchResponse = searchPage.Content;
                    var tweets = searchResponse.Tweets;
                    while (i < 20)
                    {

                        listTweets.Add(tweets[i]);
                        if (i == 20)
                            break;
                        i++;
                    }
                    break;

                }
                return listTweets;
            }
            catch (Exception)
            {

                var message = $" cannot get user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }

        public List<TweetV2> GetTweetsByUser(string username, string limit)
        {
            try
            {
                 var user = GetUsers(username);                
                var listTweets = new List<TweetV2>();
                int i = 0;
                var tweets = new List<TweetV2>();

                return user.Includes.Tweets.ToList();                
            }
            catch (Exception)
            {
                var message = $" cannot get tweet user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterException(message);
            }
        }
    }
}

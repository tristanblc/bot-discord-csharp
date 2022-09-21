using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using Tweetinvi;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

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
                throw new TwitterConsumerException(message);

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
                throw new TwitterConsumerException(message);

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

                throw new TwitterConsumerException(message);

            }
        }

        public DiscordEmbedBuilder ConvertTweetToEmbed(TweetV2 tweet)
        {
            var contents = $"";

            contents += $"Content : {tweet.Text}";
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
                throw new TwitterConsumerException(message);


            }
        }

        public List<TweetV2> GetTweetsByUser(string username, string limit)
        {
            try
            {
                var userResponse = TwitterClient.UsersV2.GetUserByNameAsync(new GetUserByNameV2Parameters(username)
                {
                    Expansions = UserResponseFields.Expansions.ALL,
                    TweetFields =
                     {
                         UserResponseFields.Tweet.Attachments,
                         UserResponseFields.Tweet.Entities,
                         UserResponseFields.Tweet.Text,
                         UserResponseFields.Tweet.PublicMetrics,
                         UserResponseFields.Tweet.PossiblySensitive
                     },
                    UserFields = UserResponseFields.User.ALL

                }).Result;

                return userResponse.Includes.Tweets.ToList();
              
                           
            }
            catch (Exception)
            {
                var message = $" cannot get tweet user from twitter";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
                
            }
        }

        public List<UserMentionV2> GetTweetsMentions(string username)
        {
            try
            {
                var user = this.GetUsers(username);

                var listTweets = new List<UserMentionV2>();
                user.Includes.Tweets.ToList().ForEach(tweet =>
                {
                    var replies = tweet.Entities.Mentions.ToList();
                    replies.ForEach(reply =>
                    {
                        listTweets.Add(reply);
                    });
                });


                return listTweets;

            }
            catch (Exception)
            {
                var message = $" cannot get replies from tweet";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }
        }

        public DiscordEmbedBuilder ConvertUserMentionToEmbed(UserMentionV2 userMention)
        {
            try
            {
                var contents = $"User mention - {userMention.Username}";
                contents += $"\n{userMention.Username}";
                contents += $"End position : {userMention.End} - Start position {userMention.Start}";
                var embed = UtilsService.CreateNewEmbed($"User mentions", DiscordColor.Azure,  contents);
                return embed;
            }
            catch (Exception)
            {
                var message = $" cannot get user mention to embed";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }
        }

        public List<TweetPublicMetricsV2> GetPublicMetrics(string username)
        {
            try
            {
                var user = this.GetUsers(username);
                var userMetrics = new List<TweetPublicMetricsV2>();
                user.Includes.Tweets.ToList().ForEach(tweet =>
                {
                    userMetrics.Add(tweet.PublicMetrics);
                   
                });
                return userMetrics;
            }
            catch (Exception)
            {
                var message = $" cannot get public metrics by tweet";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }
        }

        public List<TweetAttachmentsV2> GetAttachementFromTweet(string username)
        {
            try
            {
                var user = this.GetUsers(username);
                var tweetsAttacchements = new List<TweetAttachmentsV2>();
                user.Includes.Tweets.ToList().ForEach(tweet =>
                {
                    tweetsAttacchements.Add(tweet.Attachments);

                });
                return tweetsAttacchements;
            }
            catch (Exception)
            {

                var message = $" cannot get attacheement from tweets for user {username}";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }
        }

        public DiscordEmbedBuilder ConvertAttachementToEmbed(TweetAttachmentsV2 attachments)
        {
            try
            {
                var contents = $"Attachement : ";
                contents += $"Poll id : {attachments.PollIds}";
                contents += $"\nMedia keys :{attachments.MediaKeys}";
                var embed = UtilsService.CreateNewEmbed($"User mentions", DiscordColor.Azure, contents);
                return embed;
            }
            catch (Exception)
            {
                var message = $" cannot convert attacheement to embed";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }
        }

        public DiscordEmbedBuilder ConvertPublicStatToEmbed(TweetPublicMetricsV2 publicMetrics)
        {
            try
            {
                var contents = $"Public Metric";
                contents += $"\n Quote count = {publicMetrics.QuoteCount}";
                contents += $"\n Retweet count = {publicMetrics.RetweetCount}";
                contents += $"\n Reply count = {publicMetrics.ReplyCount}";
                var embed = UtilsService.CreateNewEmbed($"User mentions", DiscordColor.Azure, contents);
                return embed;

            }
            catch (Exception)
            {

                var message = $" cannot converts public stats to embed";
                Logger.WriteLogErrorLog(message);
                throw new TwitterConsumerException(message);
            }

        }
    }
}

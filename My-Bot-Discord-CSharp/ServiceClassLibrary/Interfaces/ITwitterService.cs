using DSharpPlus.Entities;
using Tweetinvi.Models.V2;

namespace ServiceClassLibrary.Interfaces
{
    public  interface ITwitterService
    {
        UserV2Response GetUsers(string username);
        TweetV2Response GetTweetByName(string name);

        List<TweetV2> GetTweetsByName(string name);
        List<TweetV2> GetTweetsByUser(string username, string limit);
   

        DiscordEmbedBuilder ConvertUserToEmbed(UserV2Response user);

        DiscordEmbedBuilder ConvertTweetToEmbed(TweetV2 tweet);

    }
}

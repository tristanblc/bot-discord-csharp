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

        List<UserMentionV2> GetTweetsMentions(string username);

        List<TweetPublicMetricsV2> GetPublicMetrics(string username);

        List<TweetAttachmentsV2> GetAttachementFromTweet(string username);

        List<HashtagV2> GetHastagsFromTweet(string username);

        List<string> GetUrlFromTweet(string username);

        List<TweetAnnotationV2> GetAnnotationFromTweet(string username);

        DiscordEmbedBuilder ConvertUrlToEmbed(List<string> urls);

        DiscordEmbedBuilder ConvertAnnotationToEmbed(TweetAnnotationV2 annotation);

        DiscordEmbedBuilder ConvertUserMentionToEmbed(UserMentionV2 userMention);

        DiscordEmbedBuilder ConvertUserToEmbed(UserV2Response user);

        DiscordEmbedBuilder ConvertTweetToEmbed(TweetV2 tweet);

        DiscordEmbedBuilder ConvertAttachementToEmbed(TweetAttachmentsV2 attachments);

        DiscordEmbedBuilder ConvertPublicStatToEmbed(TweetPublicMetricsV2 publicMetrics);

    }
}

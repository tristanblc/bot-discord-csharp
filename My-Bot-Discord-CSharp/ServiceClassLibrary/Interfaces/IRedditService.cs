using DSharpPlus.Entities;
using Reddit;
using Reddit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    internal interface IRedditService
    {

        string GetAuthorizationToken(string appId, string appSecret, string port,string browserPath);
        List<Post>  GetPostsFromSubRedditName(string name);

        Post GetLatestPostFromSubReddit(string subname);

        DiscordEmbedBuilder ConvertPostToDiscordEmbed(Post post);


        void OpenBrowser(string authUrl,string browserpath);


        void CloseBrowser();
        List<Post> SearchPostFromSubAndPassPhrase(string subreddit, string passPhrase);

        List<Post> GetTopDailyPost(string subreddit);

        List<Post> GetBestPostSubReddit(string subreddit);

        void UpdateBotPreferenceNSFW(bool allowNSFW);
    }
}

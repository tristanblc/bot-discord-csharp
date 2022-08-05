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

        string GetAuthorizationToken(string appId, string appSecret, string port);
        List<Post>  GetPostsFromSubRedditName(string name);

        Post GetLatestPostFromSubReddit(string subname);

        DiscordEmbedBuilder ConvertPostToDiscordEmbed(Post post);


        void OpenBrowser(string authUrl);
    }
}

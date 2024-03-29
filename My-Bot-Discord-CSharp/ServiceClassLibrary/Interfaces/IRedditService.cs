﻿using DSharpPlus.Entities;
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
        void CloseBrowser(string browser);
        List<Post> SearchPostFromSubAndPassPhrase(string subreddit, string passPhrase);

        List<Post> GetTopDailyPost(string subreddit);

        List<Post> GetBestPostSubReddit(string subreddit);


        public List<Post> GetHotPostFromSub(string subname);

        void UpdateBotPreferenceNSFW(bool allowNSFW);


        Post GetPostFromSub(string title, string subreddit);

        List<Comment> GetCommentsFromPost(Post post);

        DiscordEmbedBuilder ConvertCommmentToDiscordEmbed(Comment comment);


        DiscordEmbedBuilder GetCountReplies(Post post);

        DiscordEmbedBuilder ConvertUserInfoToEmbed(User user);


        User GetUser(string username);

        bool IsNotUsedUsername(string username);

        List<Post> GetUserPosts(User user);
        
        

    }
}

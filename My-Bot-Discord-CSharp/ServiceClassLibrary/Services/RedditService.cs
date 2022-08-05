using AutoMapper;
using DSharpPlus.Entities;
using ExceptionClassLibrary;
using Microsoft.Extensions.Configuration;
using Reddit;
using Reddit.AuthTokenRetriever;
using Reddit.Controllers;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class RedditService : IRedditService
    {
        
        private RedditClient RedditClient { get; set; }
        
        private UtilsService UtilsService { get; set; }

        private IMapper Mapper { get; set; }

        private string BrowserPath { get; init; }

        public RedditService(string appId,string appSecret,string browserPath)

        {
            BrowserPath = browserPath;

            var token = this.GetAuthorizationToken(appId, appSecret, "8080");


            RedditClient = new RedditClient(appId, token,appSecret);

            UtilsService = new UtilsService();
        }

        public List<Post> GetPostsFromSubRedditName(string name)
        {
            try
            {

                return RedditClient.Subreddit(name).Posts.New;
            }
          
            catch(Exception ex)
            {
                throw new RedditException($"Error cannot get from sub reddit named r/{name}");
            }
        }


        public Post GetLatestPostFromSubReddit(string subname)
        {
            try
            {
                return this.GetPostsFromSubRedditName(subname).ToList().First();

            }
            catch(Exception ex)
            {
                throw new RedditException($"cannot get latest post from r/{subname}");
            }
          
        }

        public DiscordEmbedBuilder ConvertPostToDiscordEmbed(Post post)
        {
            try
            {
                var contents = $"Author: {post.Author} Comments: {post.Comments} Created at : {post.Created}";

                return UtilsService.CreateNewEmbed($"{post.Title}",DiscordColor.Aquamarine,contents);
       

            }
            catch(Exception ex)
            {
                throw new RedditException("cannot convert to discordembed");
            }
           
        }

        public string GetAuthorizationToken(string appId, string appSecret, string port)
        {
            try
            {



                AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, appSecret, 800);


                authTokenRetrieverLib.AwaitCallback();
           

                OpenBrowser(authTokenRetrieverLib.AuthURL());
                // Replace this with whatever you want the app to do while it waits for the user to load the auth page and click Accept.  --Kris
                while (authTokenRetrieverLib.RefreshToken == null)
                {
                    //Console.WriteLine(authTokenRetrieverLib.RefreshToken);
                }
                

                // Cleanup.  --Kris
                authTokenRetrieverLib.StopListening();

                return authTokenRetrieverLib.RefreshToken;
            }
            catch(Exception ex)
            {
                throw new RedditException("cannot get refresh token");
            }
            throw new NotImplementedException();
        }

        public void OpenBrowser(string authUrl)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe")
                {
                    Arguments = authUrl

                };
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new Exception("erroe");
            }
        }
    }
}

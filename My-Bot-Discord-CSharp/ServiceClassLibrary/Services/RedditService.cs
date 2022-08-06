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
        
        private ILoggerProject LoggerProject { get; init; }

        public RedditService(string appId,string appSecret,string browserPath)
        {
            BrowserPath = browserPath;
            LoggerProject = new LoggerProject();
            
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
                var exception_message = $"cannot get latest post from r/{subname}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
          
        }

        public DiscordEmbedBuilder ConvertPostToDiscordEmbed(Post post)
        {
            try
            {
                var contents = $"Author: {post.Author} - Created at : {post.Created} \n";
                contents += $"https://www.reddit.com/{post.Permalink}";
                var embed  = UtilsService.CreateNewEmbed($"{post.Title}",DiscordColor.Aquamarine,contents);
                return embed;
       

            }
            catch(Exception ex)
            {
                var exception_message = "cannot convert to discordembed";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
           
        }

        public string GetAuthorizationToken(string appId, string appSecret, string port)
        {
            try
            {



                AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, appSecret, 800);


                authTokenRetrieverLib.AwaitCallback();
           

                OpenBrowser(authTokenRetrieverLib.AuthURL());
                LoggerProject.WriteInformationLog($"Finding reddit token");
                while (authTokenRetrieverLib.RefreshToken == null)
                {
                  
                }                

             
                authTokenRetrieverLib.StopListening();
                LoggerProject.WriteInformationLog($"Program have reddit token - token : {authTokenRetrieverLib.RefreshToken}");
                return authTokenRetrieverLib.RefreshToken;
            }
            catch(Exception ex)
            {
                var exception_message = "cannot get refresh token";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
              
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
                var exception_message = "cannot load browser in order to get reddit refreshed token";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);

               
            }
        }
    }
}

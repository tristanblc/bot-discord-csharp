using AutoMapper;
using DSharpPlus.Entities;
using ExceptionClassLibrary;
using Microsoft.Extensions.Configuration;
using Reddit;
using Reddit.AuthTokenRetriever;
using Reddit.Controllers;
using Reddit.Inputs.Search;
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

        private string pId { get; set; }

        public RedditService(string appId, string appSecret, string browserPath)
        {
            BrowserPath = browserPath;
            LoggerProject = new LoggerProject();

            BrowserPath = browserPath;

            var token = this.GetAuthorizationToken(appId, appSecret, "8080", browserPath);


            RedditClient = new RedditClient(appId, token, appSecret);

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
                contents += $"https://www.reddit.com{post.Permalink}";
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

        public string GetAuthorizationToken(string appId, string appSecret, string port,string browserpath)
        {
            try
            {



                AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, appSecret, 800);


                authTokenRetrieverLib.AwaitCallback();
           

                OpenBrowser(authTokenRetrieverLib.AuthURL(),browserpath);

                LoggerProject.WriteInformationLog($"Finding reddit token");

                while (authTokenRetrieverLib.RefreshToken == null)
                {                  
                  
                }              


             
                authTokenRetrieverLib.StopListening();

                CloseBrowser(browserpath);

                LoggerProject.WriteInformationLog($"Program have reddit token - token : {authTokenRetrieverLib.RefreshToken}");

                return authTokenRetrieverLib.RefreshToken;
            }
            catch(Exception ex)
            {

                var exception_message = "cannot get refresh token";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
              

                throw new RedditException("cannot get refresh token");

            }
          
        }

        public void OpenBrowser(string authUrl,string browser)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(browser)
                {
                    Arguments = authUrl

                };

                var process = Process.Start(processStartInfo);

                pId = process.Id.ToString();
               
            }
            catch (System.ComponentModel.Win32Exception)
            {

                var exception_message = "cannot load browser in order to get reddit refreshed token";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
              

            }
        }

        public void CloseBrowser(string browser)
        {

            string filename = browser.Split("\\").Last();
            filename = filename.Substring(0,filename.Length - 4);


            try
            {
                Process[] runningProcesses = Process.GetProcesses();
                foreach (Process process in runningProcesses)
                {
                    if(process.ProcessName == filename)
                    {
                        process.Kill();
                        LoggerProject.WriteInformationLog($"Kill browser process Id: {process.Id} - {process.ProcessName}");
                    }
                       
                }
            }
            catch(Exception ex)
            {
                throw new RedditException("Cannot stop browse process");
            }
        }

        public List<Post> SearchPostFromSubAndPassPhrase(string subreddit, string passPhrase)
        {
            var posts = new List<Post>();

            try
            {

                return RedditClient.Subreddit(subreddit).Search(new SearchGetSearchInput(passPhrase));
               
            }
            catch(Exception ex)
            {
                var exception_message = $"cannot load search on r/{subreddit} & pass phrase : {passPhrase}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
            
            return posts;
        }

        public List<Post> GetTopDailyPost(string subreddit)
        {
            var sub = RedditClient.Subreddit(subreddit);
            var today = DateTime.Now;

            List<Post> posts = new List<Post>();


            try
            {

                return sub.Posts.GetTop(subreddit, "", "", 50, false).ToList();
                   

            }
            catch(Exception ex)
            {
                var exception_message = $"cannot load top of subreddit r/{subreddit}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
       
         

            return posts;

        }

        public List<Post> GetBestPostSubReddit(string subreddit)
        {
            var sub = RedditClient.Subreddit(subreddit);

            List<Post> posts = new List<Post>();


            try
            {
                return sub.Posts.GetBest(subreddit, "", 10, false).ToList();
            }
            catch (Exception ex)
            {
                var exception_message = $"cannot load top of subreddit r/{subreddit}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
            
            return posts;
        }

       

        public void UpdateBotPreferenceNSFW(bool allowNSFW)
        {
            try
            {
                var pref = RedditClient.Account.Prefs();
                pref.LabelNSFW  = allowNSFW;
                RedditClient.Account.UpdatePrefs(new Reddit.Things.AccountPrefsSubmit(pref, null, false, null));
            }
            catch(Exception ex)
            {
                var exception_message = "cannot change nsfw status on reddit preference";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
      
            }
        }

        public List<Post> GetHotPostFromSub(string subname)
        {
            var sub = RedditClient.Subreddit(subname);

            
            try
            {
                return sub.Posts.GetHot(subname, "", "", 10, false);
            }
            catch(Exception ex)
            {
                var exception_message = "cannot get hot posts from /r{subname}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);

            }
        }

        public Post GetPostFromSub(string title, string subreddit)
        {
            var sub = RedditClient.Subreddit(subreddit);
            try
            {
                return RedditClient.Subreddit(subreddit).Posts.New.Where(post => post.Title.CompareTo(title) >= 0).First();


            }
            catch (Exception ex)
            {

                var exception_message =$"can't get from subreddit r/{subreddit} & title { title}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);

            }


        }

        public List<Comment> GetCommentsFromPost(Post post)
        {
            return post.Comments.GetRandom().ToList();

        }

        public DiscordEmbedBuilder ConvertCommmentToDiscordEmbed(Comment commment)
        {
            try
            {
                var contents = $"Author: {commment.Author}";
                contents += $"    {commment.Body}";
                var embed = UtilsService.CreateNewEmbed($"{commment.ParentFullname}", DiscordColor.Aquamarine, contents);
                return embed;


            }
            catch (Exception ex)
            {
                var exception_message = "cannot convert to discordembed";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
        }

        public DiscordEmbedBuilder GetCountReplies(Post post)
        {
            try
            {

                var count =  post.Comments.GetComments("").Count;

                var embed = UtilsService.CreateNewEmbed($"Number of replies on {post.Title}", DiscordColor.Aquamarine, count.ToString());

                return embed;

            }
            catch(Exception ex)
            {
                var exception_message = $"cannot count post : {post.Fullname} replies";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);          
            }
        }

        public DiscordEmbedBuilder ConvertUserInfoToEmbed(User user)
        {
            try
            {
                var contents = $"Username :  {user.Name}";
                contents += $"Friend number : {user.NumFriends}";
                contents += $"Karma link : {user.LinkKarma}";
                contents += $"{user.Created.ToString()}";
                var embed = UtilsService.CreateNewEmbed($"User data user {user.Name}", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch(Exception ex)
            {
                var exception_message = $"cannot convert to discord embed";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
         
        }

        public User GetUser(string username)
        {
            try
            {
                    return RedditClient.SearchUsers(new SearchGetSearchInput(username)).ToList().First();

            }
            catch(Exception ex)
            {
                var exception_message = $"cannot return user from username : {username}";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
          
        }

        public bool IsNotUsedUsername(string username)
        {
            try
            {
                return RedditClient.SearchUsers(new SearchGetSearchInput(username)).Count() != 0 ? false : true;
            }
            catch(Exception ex)
            {
                var exception_message = $"cannot convert to discord embed";
                LoggerProject.WriteLogErrorLog(exception_message);
                return false;
            }
           
        }

        public List<Post> GetUserPosts(User user)
        {
            try
            {
                return user.PostHistory.ToList();
            }
            catch(Exception ex)
            {
                var 
                    exception_message = $"cannot return uuser posts";
                LoggerProject.WriteLogErrorLog(exception_message);
                throw new RedditException(exception_message);
            }
        }
    }
}

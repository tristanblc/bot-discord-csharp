using AutoMapper;
using DSharpPlus.Entities;
using ExceptionClassLibrary;
using Microsoft.Extensions.Configuration;
using Reddit;
using Reddit.Controllers;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
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

        public RedditService(string appId,string token)
        {
            this.Connect(appId,token);
            UtilsService = new UtilsService();
        }

        internal void Connect(string appId, string refreshedToken)
        {
            RedditClient = new RedditClient(appId, refreshedToken);
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
    }
}

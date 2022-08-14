using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix;

namespace ServiceClassLibrary.Services
{
    public  class TwitchService : ITwitchService
    {
        private TwitchAPI TwitchClient { get; init; }
        private ILoggerProject LoggerProject { get; init; }

        public TwitchService(string clientid,string accesstoken)
        {
            TwitchClient = Connect(clientid, accesstoken);
            LoggerProject = new LoggerProject();

        }

        public TwitchAPI Connect(string clientid, string accesstoken)
        {
            try
            {
                var client = new TwitchAPI();
                client.Settings.ClientId = clientid;
                client.Settings.AccessToken = accesstoken;
                return client;
            }
            catch(Exception ex)
            {
                var exception = $"Cannot connect to twitch api";
                LoggerProject.WriteInformationLog(exception);
                throw new TwitchAPIException(exception);

            }
        
        }

        public Users GetUserTwitch(string username)
        {
            throw new NotImplementedException();
        }

        public Stream GetStreamByStreamerUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Users GetFollowedUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}

using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    internal class SteamVideoService : ISteamVideoService
    {

        private SteamWebInterfaceFactory SteamWebInterface { get; init; }

        private ILoggerProject LoggerProject { get; init; }

        private IUtilsService UtilsService { get; init; }
        private HttpClient HttpClient { get; init; }

        public  ISteamUser SteamUser { get; init; }

        public SteamVideoService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
            SteamUser = this.GetISteamUser(HttpClient);


        }

        public SteamWebInterfaceFactory GetClient(string apikey)
        {
            try
            {
                return new SteamWebInterfaceFactory(apikey);

            }
            catch (Exception ex)
            {

                var message = "Cannot get construct client";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }

        }

        public ISteamUser GetISteamUser(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamUser>(httpClient);


            }
            catch (Exception ex)
            {

                var message = $"Cannot get ISteamStore";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public string GetReplayURL(string videoId,string userId)
        {
            try
            {
                return SteamUser.GetCommunityProfileAsync(userId)

            }
            catch(Exception ex)
            {
                var message = $"Cannot get ISteamStore";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }
    }
}

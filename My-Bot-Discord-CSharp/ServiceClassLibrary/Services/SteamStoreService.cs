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
    internal class SteamStoreService : ISteamStoreService
    {

        private SteamWebInterfaceFactory SteamWebInterface { get; init; }

        private ILoggerProject LoggerProject { get; init; }

        private IUtilsService UtilsService { get; init; }
        private HttpClient HttpClient { get; init; }

        public SteamStore SteamStore { get; init; }

        public SteamStoreService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
            SteamStore = this.GetISteamStore(HttpClient);


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

        public SteamStore GetISteamStore(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamStoreInterface(httpClient);


            }
            catch (Exception ex)
            {

                var message = $"Cannot get ISteamStore";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }
    }
}

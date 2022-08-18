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
    internal class SteamGameService : ISteamGameService
    {

        private SteamWebInterfaceFactory SteamWebInterface { get; init; }

        private ILoggerProject LoggerProject { get; init; }

        private IUtilsService UtilsService { get; init; }
        private HttpClient HttpClient { get; init; }

        public ICSGOServers SteamCSGOServers { get; init; }
        public IDOTA2Match DotaMatchStats { get; init; }
        public ISteamEconomy ICSGOServers { get; init; }
        public ITFItems TFItems { get; init; }

        public SteamGameService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
       

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

        public IDOTA2Match GetIDota(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<DOTA2Match>(httpClient);
            }
            catch(Exception ex)
            {
                var message = "Cannot get dota match server interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
   
        }

        public ICSGOServers GetCSGOServer(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<CSGOServers>(httpClient);
            }
            catch (Exception ex)
            {
                var message = "Cannot get csgo server interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
         
        }

        public ISteamEconomy GetISteamEconomy(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamEconomy>(httpClient);
            }
            catch (Exception ex)
            {
                var message = "Cannot get steam economy interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
   
        }

        public ITFItems GetITFItems(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<TFItems>(httpClient);
            }
            catch (Exception ex)
            {
                var message = "Cannot get steam econmy";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
          
        }
    }
}

using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using Steam.Models.CSGO;
using Steam.Models.SteamEconomy;
using Steam.Models.TF2;
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
        public ISteamEconomy SteamEconomy{ get; init; }

        public ITFItems TFItems { get; init; }

        public SteamService SteamService { get; init; }


        public SteamGameService(string apikey)
        {
            SteamWebInterface = this.GetClient(apikey);
            LoggerProject = new LoggerProject();
            UtilsService = new UtilsService();
            HttpClient = new HttpClient();
            SteamCSGOServers = this.GetCSGOServer(HttpClient);
            SteamEconomy = this.GetISteamEconomy(HttpClient);       
            SteamService = new SteamService(apikey);
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

        public ITFItems GetITFitems(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<TFItems>(httpClient);
            }
            catch(Exception ex)
            {
                var message = "Cannot get steam economy interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public ServerStatusModel GetServerStatus()
        {
            try
            {
                return SteamCSGOServers.GetGameServerStatusAsync().Result.Data;
            }
            catch (Exception ex)
            {

                var message = "Cannot get steam economy interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
       
        }

        public AssetClassInfoResultModel GetAssetInfo(string appname)
        {
            try
            {
                var app = SteamService.GetSteamAppByName(appname).First();
                return SteamEconomy.GetAssetClassInfoAsync(app.AppId, new List<ulong>() { }, "en").Result.Data;
            }
            catch (Exception)
            {
                var message = "Cannot get  get price";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public AssetPriceResultModel GetPriceAssset(string appname, string currency)
        {
            try
            {
                var app = SteamService.GetSteamAppByName(appname).First();
                return SteamEconomy.GetAssetPricesAsync(app.AppId,currency,"en").Result.Data;
            }
            catch (Exception)
            {
                var message = "Cannot get  get price";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
      
        }

        public List<GoldenWrenchModel> GetGoldenWrenchModels()
        {
            try
            {
                return TFItems.GetGoldenWrenchesAsync().Result.Data.ToList();
            }
            catch(Exception ex)
            {
                var message = "Cannot get wrenchs models";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }

        }

        public DiscordEmbedBuilder ConvertServerStatusToEmbed(ServerStatusModel serverStatus)
        {
            throw new NotImplementedException();
        }

        public DiscordEmbedBuilder ConvertAssetPriceResultToEmbed(AssetPriceResultModel asset)
        {
            throw new NotImplementedException();
        }

        public DiscordEmbedBuilder ConvertAssetClassToEmbed(AssetClassInfoResultModel asset)
        {
            throw new NotImplementedException();
        }
    }
}

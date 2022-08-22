using DSharpPlus.Entities;
using ExceptionClassLibrary;
using ServiceClassLibrary.Interfaces;
using Steam.Models;
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
    public  class SteamGameService : ISteamGameService
    {

        private SteamWebInterfaceFactory SteamWebInterface { get; init; }

        private ILoggerProject LoggerProject { get; init; }

        private IUtilsService UtilsService { get; init; }
        private HttpClient HttpClient { get; init; }

        public ICSGOServers SteamCSGOServers { get; init; }
        public ISteamEconomy SteamEconomy{ get; init; }

        public ITFItems TFItems { get; init; }

        private ISteamApps SteamApps { get; init; }
        private ISteamWebAPIUtil SteamWebAPIUtil { get; init; } 
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
            TFItems = this.GetITFitems(HttpClient);

            SteamApps = this.GetISteamApps(HttpClient);
            SteamWebAPIUtil = this.GetISteamWebAPIUtil(HttpClient);

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
            try
            {
                var contents = $"Get status f servers";
                contents += $"\nNumber of datacenters : {serverStatus.Datacenters.Count}";
                contents += $"\nNumber of steam community server :{serverStatus.Services.SteamCommunity.Length}";

                contents += $"\nOnline users {serverStatus.Matchmaking.OnlinePlayers}";
                contents += $"\nNumber of online servers :{serverStatus.Matchmaking.OnlineServers}";
                contents += $" App TimeStamp :{serverStatus.App.Timestamp}";
                contents += $"\nCsgo version :{serverStatus.App.Version}";
                var embed = UtilsService.CreateNewEmbed($"Server", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch (Exception ex)
            {
                var message = "Cannot get embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

     
        public DiscordEmbedBuilder ConvertAssetClassToEmbed(AssetClassInfoResultModel asset)
        {
            try
            {
                var contents = $"Get assets";
                contents += $"\nNumber of assets : {asset.AssetClasses.Count}";
                var embed = UtilsService.CreateNewEmbed($"asset", DiscordColor.Aquamarine, contents);
                return embed;

            }
            catch (Exception ex)
            {
                var message = "Cannot get embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public DiscordEmbedBuilder ConvertGoldenWrenchToEmbed(GoldenWrenchModel goldenWrench)
        {
            try
            {
                var contents = $"Get golden wrench info :";
                contents += $"\nsSteamId {goldenWrench.SteamId}";
                contents += $"\nNumber : {goldenWrench.WrenchNumber}";
                contents += $"\nsItemId {goldenWrench.ItemId}";
                var embed = UtilsService.CreateNewEmbed($"asset", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch (Exception)
            {
                var message = "Cannot get embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public ISteamApps GetISteamApps(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamApps>(httpClient);
            }
            catch(Exception ex)
            {
                var message = "Cannot get steam apps interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }           
        }

        public SteamServerInfoModel GetServerApiInfo()
        {
            try
            {
                return SteamWebAPIUtil.GetServerInfoAsync().Result.Data;
            }
            catch (Exception ex)
            {
                var message = "Cannot get steam apps interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
        }

        public List<SteamInterfaceModel> GetSuppportedApiList()
        {
            try
            {
                return SteamWebAPIUtil.GetSupportedAPIListAsync().Result.Data.ToList();
            }
            catch(Exception ex)
            {
                
                    var message = "Cannot get supported api";
                    LoggerProject.WriteLogErrorLog(message);
                    throw new SteamException(message);
            }
        }

        public DiscordEmbedBuilder ConvertServerStatusToEmbed(SteamServerInfoModel steamServerModel)
        {
            try
            {
                var contents = $"";
                contents += $"Server time {steamServerModel.ServerTime.ToDateTime().ToLocalTime()}";
                contents += $"Datetime server : {steamServerModel.ServerTimeDateTime}";
                var embed = UtilsService.CreateNewEmbed("Server status ", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch(Exception ex)
            {
                var message = "Cannot get embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }
        }

        public DiscordEmbedBuilder ConvertSteamInterfaceToEmbed(SteamInterfaceModel steamInterfaceModel)
        {
            try
            {
                var contents = $"";
                contents += $"{steamInterfaceModel.Name}";
                steamInterfaceModel.Methods.ToList().ForEach(myInterface =>
                {
                    contents += $"{myInterface.Name}  - {myInterface.Version}";

                });

                var embed = UtilsService.CreateNewEmbed($"Interface steam api", DiscordColor.Aquamarine, contents);
                return embed;
            }
            catch(Exception ex)
            {
                var message = "Cannot get embed";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);

            }
           
        }

        public ISteamWebAPIUtil GetISteamWebAPIUtil(HttpClient httpClient)
        {
            try
            {
                return SteamWebInterface.CreateSteamWebInterface<SteamWebAPIUtil>(HttpClient);
            }
            catch(Exception ex)
            {
                var message = "Cannot get steam web aoi interface";
                LoggerProject.WriteLogErrorLog(message);
                throw new SteamException(message);
            }
      
        }
    }
}

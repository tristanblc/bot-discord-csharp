using DSharpPlus;
using ExceptionClassLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using My_Bot_Discord_CSharp.Services.Interface;
using My_Bot_Discord_CSharp.Services.Utils;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services
{
    public class Startup : IStartupCredential
    {
        private ConfigurationSettings _configuration { get; init; }

        private ILoggerProject LoggerProject { get; init; }
        public Startup()
        {
            _configuration = new ConfigurationSettings();
            LoggerProject = new LoggerProject();
        }


        public string GetTokenFromJsonFile()
        {
            try
            {
                LoggerProject.WriteInformationLog("Retrieve discord token on appsettings.json");
                return _configuration.DiscordToken;
            }
            catch (ProjectConfigurationException ex)
            {
                LoggerProject.WriteInformationLog("Invalid Token or :" + ex.ToString());
                throw new ProjectConfigurationException("Invalid Token or :" + ex.ToString());

            }


        }

    
    }
}

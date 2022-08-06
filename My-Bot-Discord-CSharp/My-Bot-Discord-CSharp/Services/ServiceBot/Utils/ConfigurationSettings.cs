using ExceptionClassLibrary;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using My_Bot_Discord_CSharp.Services.Interface;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Utils
{
    internal class ConfigurationSettings : IConfigureSetting
    {

        private readonly IConfiguration configuration;


        public string DiscordToken { get; private set ; }
        public string LavalinkPassword { get; private set; }       

        public int Port { get; private set; }
        public string HostName { get; private set; }

        private ILoggerProject LoggerProject { get; init; }

        public void ConfigureServices()
        {
            try
            {                
                var builder = new ConfigurationBuilder()                             
                              .AddJsonFile("app.json", optional: false, reloadOnChange: true)
                              .AddEnvironmentVariables();

                IConfiguration config = builder.Build();

                DiscordToken = config.GetSection("Token").Value.ToString();
           
            }
            catch(Exception ex)
            {
                throw new ProjectConfigurationException("Error : Token not valid");

            }
         
        }

        public ConfigurationSettings()
        {
            LoggerProject = new LoggerProject();
            try
            {                                
                ConfigureServices();
                LoggerProject.WriteInformationLog("Configuration services initialized");
            }
            catch(ProjectConfigurationException ex)
            {
                LoggerProject.WriteLogErrorLog(ex.Message);                
                throw ex;
            }
           
        }

       
    }
}

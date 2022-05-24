using DSharpPlus;
using ExceptionClassLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using My_Bot_Discord_CSharp.Services.Interface;
using My_Bot_Discord_CSharp.Services.Utils;
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

        public Startup()
        {
            _configuration = new ConfigurationSettings();
        }


        public string GetTokenFromJsonFile()
        {
            try
            {
                return _configuration.DiscordToken;
            }
            catch (ProjectConfigurationException ex)
            {
                throw new ProjectConfigurationException("Invalid Token or :" + ex.ToString());

            }


        }

        public string GetLavalinkPassword()
        {
            try
            {
                return _configuration.LavalinkPassword;
            }
            catch (ProjectConfigurationException ex)
            {
                throw new ProjectConfigurationException("Invalid Token or :" + ex.ToString());

            }
        }
        public string GetHostName()
        {
            try
            {
                return _configuration.HostName;
            }
            catch (ProjectConfigurationException ex)
            {
                throw new ProjectConfigurationException("Invalid Token or :" + ex.ToString());

            }
        }
        public int GetPort()
        {
            try
            {
                return _configuration.Port;
            }
            catch (ProjectConfigurationException ex)
            {
                throw new ProjectConfigurationException("Invalid Token or :" + ex.ToString());

            }
        }
    }
}

using DSharpPlus;
using DSharpPlus.Lavalink;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using My_Bot_Discord_CSharp.Services;
using My_Bot_Discord_CSharp.Services.ServiceLavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.ServiceBot
{

    [TestClass]
    internal class ServiceLavalinkTest
    {
        private DiscordClient Client { get; set; }

        private Startup Startup { get; set; }

        private LavalinkService lavalinkService { get; set; }


        public ServiceLavalinkTest(DiscordClient Client , Startup Startup)
        {
            lavalinkService = new LavalinkService(Client, Startup);

        }

        [TestMethod]
        public void CreateLavalinkConfigTest()
        {

            var resultat = lavalinkService.CreateLavalinkConfig();

            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(LavalinkConfiguration));
           

        }


    }
}

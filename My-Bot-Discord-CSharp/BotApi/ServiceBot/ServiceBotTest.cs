using DSharpPlus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using My_Bot_Discord_CSharp.Services.ServiceBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.ServiceBot
{
    [TestClass]
    internal class ServiceBotTest
    {

        private DiscordClientService discordClient { get; set; }

        public ServiceBotTest()
        {
            discordClient = new DiscordClientService();
        }

        [TestMethod]
        public void CreateDiscordClientTest()
        {
            var resultat = discordClient.CreateDiscordClient();
            Assert.IsNotNull(resultat);
            Assert.IsInstanceOfType(resultat, typeof(DiscordClient));

        }

    }
}

using DSharpPlus;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using My_Bot_Discord_CSharp.Services.Exceptions;
using My_Bot_Discord_CSharp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.ServiceLavalink
{
    public class LavalinkService : IServiceLavalink
    {
        private  DiscordClient discordClient { get; init; }
        private Startup Startup { get; init ; }

        public LavalinkService(DiscordClient client, Startup startup)
        {
            discordClient = client != null ?  client :  throw new MyLavalinkException("Error client") ;
            Startup = new Startup();


        }

        public LavalinkConfiguration CreateLavalinkConfig()
        {

            var endpoint = new ConnectionEndpoint
            {
                Hostname = Startup.GetHostName(), // From your server configuration.
                Port = Startup.GetPort(), // From your server configuration
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = Startup.GetLavalinkPassword(), // From your server configuration.
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };
            return lavalinkConfig;
        }

    }
}

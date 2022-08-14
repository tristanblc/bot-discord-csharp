using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix;

namespace ServiceClassLibrary.Interfaces
{
    public interface ITwitchService
    {
        TwitchAPI Connect(string clientid, string accesstoken);

        Users GetUserTwitch(string username);

        Stream GetStreamByStreamerUsername(string username);

        Users GetFollowedUser(string username);
    }
}

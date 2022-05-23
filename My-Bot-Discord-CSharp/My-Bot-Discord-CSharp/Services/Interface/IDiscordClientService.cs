using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Interface
{
    internal interface IDiscordClientService
    {
        DiscordClient CreateDiscordClient();
    }
}

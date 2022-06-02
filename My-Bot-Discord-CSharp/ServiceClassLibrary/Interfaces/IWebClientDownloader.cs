using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    internal interface IWebClientDownloader
    {
        void DownloadVideoFromDiscord(DiscordAttachment discordAttachement);

        Stream ConvertVideoToStream(string path);
    }
}

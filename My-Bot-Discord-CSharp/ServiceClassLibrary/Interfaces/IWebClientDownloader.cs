using DSharpPlus.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Clips.GetClips;

namespace ServiceClassLibrary.Interfaces
{
    internal interface IWebClientDownloader
    {
        void DownloadVideoFromDiscord(DiscordAttachment discordAttachement);
        FileStream ConvertVideoToStream(string path);
        void DownloadVideo(string url, string name);
    
    }
}

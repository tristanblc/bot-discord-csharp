﻿using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IVideoService
    {
        void UploadVideoOnDirectory(DiscordAttachment discordAttachment);

        string CompressVideo(DiscordAttachment discordAttachement);

        string GetVideoInfo(DiscordAttachment discordAttachement);

        void DeleteVideo(string path);

        string ExtractAudioFromVideo(DiscordAttachment discordAttachment);

    }
}

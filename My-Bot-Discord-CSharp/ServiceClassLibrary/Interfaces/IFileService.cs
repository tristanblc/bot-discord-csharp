using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IFileService
    {
        void SaveFile(string fileUrl, string filename);

        void DeleteFile(string filename);

        void WriteTxt(List<DiscordMessage> messages, string filename);

    }
}

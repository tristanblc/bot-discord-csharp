using DSharpPlus.Entities;
using HtmlAgilityPack;
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
        void SavePDFFile(HtmlDocument doc,string filename);

        void DeleteFile(string filename);

        void WriteTxt(List<DiscordMessage> messages, string filename);


        string Compress2Zip(List<DiscordAttachment> attachements, string filename);


        List<string> Decompress2File(DiscordAttachment attachment);

        void WriteJson(List<DiscordMessage> messages, string filename);


    }
}

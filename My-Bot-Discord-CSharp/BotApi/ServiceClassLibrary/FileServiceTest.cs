using DSharpPlus.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.ServiceClassLibrary
{
    [TestClass]
    internal class FileServiceTest
    {
        private IFileService fileService { get; set; }

        public FileServiceTest()
        {
            fileService = new FileService();
        }

        [TestMethod]
        public void TestSaveFile()
        {

            var filename = "test.txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathFile = Path.Join(path, filename);

            fileService.SaveFile(pathFile,filename);


            int fCount = Directory.GetFiles(pathFile, "*", SearchOption.AllDirectories).Length;


            Assert.IsNotNull(fCount);

            Assert.AreEqual(1, fCount);

        }


        [TestMethod]
        public void TestDeleteFile()
        {
            var filename = "test.txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");

            var pathFile = Path.Join(path, filename);

            fileService.DeleteFile(pathFile);


            int fCount = Directory.GetFiles(pathFile, "*", SearchOption.AllDirectories).Length;


            Assert.IsNotNull(fCount);

            Assert.AreEqual(0, fCount);


        }

        public void TestWriteTxt()
        {
            List<DiscordMessage> listDiscordMessage = new List<DiscordMessage>();


            fileService.WriteTxt(listDiscordMessage, "test.txt");


            var path = Path.Combine(Directory.GetCurrentDirectory(), "documents");
            var pathFile = Path.Combine(path, "test.txt");

            int fCount = Directory.GetFiles(pathFile, "*", SearchOption.AllDirectories).Length;


            Assert.IsNotNull(fCount);

            Assert.AreEqual(0, fCount);


        }
    }
}

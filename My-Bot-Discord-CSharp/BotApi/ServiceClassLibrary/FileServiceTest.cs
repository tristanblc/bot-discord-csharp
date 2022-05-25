using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
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
  

        }


        [TestMethod]
        public void TestDeleteFile()
        {

          
        }

        public void TestWriteTxt()
        {
           
        }
    }
}

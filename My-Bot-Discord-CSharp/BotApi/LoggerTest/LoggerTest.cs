using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.LoggerTest
{
    [TestClass]
    internal class ServiceBotTest 
    {
        private ILoggerProject LoggerProject { get; init; }
        public ServiceBotTest()
        {
            LoggerProject = new LoggerProject();
        }

        [TestMethod]     
        public void WriteLogWarningLogTest(string message)
        {
            LoggerProject loggerProject = new LoggerProject();


            loggerProject.WriteLogWarningLog(message);

            Assert.Fail(message);
        }
        [TestMethod]
        public void WriteLogErrorLogTest(string message)
        {
            LoggerProject loggerProject = new LoggerProject();


            loggerProject.WriteLogErrorLog(message);

            Assert.Fail(message);
        }

        [TestMethod]
        public void WriteInformationLogTest(string message)
        {
            LoggerProject loggerProject = new LoggerProject();


            loggerProject.WriteInformationLog(message);

            Assert.Fail(message);
        }
    }
}

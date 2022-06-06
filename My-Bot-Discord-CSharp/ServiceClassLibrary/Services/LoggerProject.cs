using Serilog;
using Serilog.Core;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    internal class LoggerProject : ILoggerProject
    {

        private Logger Logger{ get; init; } 

        public LoggerProject()
        {
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Month)
                .CreateLogger();
        }

        public void WriteLogWarningLog(string message)
        {
            Logger.Warning(message);
        }

        public void WriteLogErrorLog(string message)
        {
            Logger.Error(message);
        }

        public void WriteInformationLog(string message)
        {
            Logger.Information(message);
        }
    }
}

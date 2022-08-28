using ExceptionClassLibrary;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceClassLibrary.Services
{
    public class ServerInfoService : IServerInfoService
    {
        private ILoggerProject LoggerProject { get; init; }

        public ServerInfoService()
        { 
            LoggerProject = new LoggerProject();
        }
        public string GetMemeryUsedInformation(int occ)
        {
            try
            {
                int i = 0;
                var value = $"";
                while (i < occ)
                {

                    Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                    long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;


                    value += $"\nRam used : {totalBytesOfMemoryUsed / 1024 / 1000} MB";
                    i++;
                    Thread.Sleep(1000);
                }
                return value;


            }
            catch(Exception ex)
            {
                var exception = $"cannot get used memory";
                LoggerProject.WriteLogErrorLog(exception);
                throw new ServerInfoException(exception);
            }
       
        }

        public string GetProcessorPerformamceUsed(int occ)
        {
            try
            {
                int i = 0;
                var value = $"";
                while (i < occ)
                {

                    Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                    var totalBytesOfMemoryUsed = currentProcess.ProcessorAffinity;


                    value += $"\nProc used : {totalBytesOfMemoryUsed.ToInt64() /100} %";
                    i++;
                    Thread.Sleep(1000);
                }
                return value;

            }
            catch (Exception ex)
            {
                var exception = $"cannot get used proc pourcentage";
                LoggerProject.WriteLogErrorLog(exception);
                throw new ServerInfoException(exception);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface ILoggerProject
    {
        void WriteLogWarningLog(string message);
        void WriteLogErrorLog(string message);
        void WriteInformationLog(string message);

    }
}

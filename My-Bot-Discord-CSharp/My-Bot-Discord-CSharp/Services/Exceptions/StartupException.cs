using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Exceptions
{
    internal class StartupException : Exception
    {
        public StartupException() { }

        public StartupException(string message)
            : base(message) { }

        public StartupException(string message, Exception inner)
            : base(message, inner) { }
    }
}

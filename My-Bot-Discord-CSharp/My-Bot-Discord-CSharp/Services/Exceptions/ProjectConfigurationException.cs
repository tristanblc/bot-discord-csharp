using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Exceptions
{
    internal class ProjectConfigurationException : Exception
    {
        public ProjectConfigurationException() { }

        public ProjectConfigurationException(string message)
            : base(message) { }

        public ProjectConfigurationException(string message, Exception inner)

            : base(message, inner) { }
    }
}

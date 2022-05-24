using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class ProjectConfigurationException : Exception
    {
        public ProjectConfigurationException() { }

        public ProjectConfigurationException(string message)
            : base(message) { }

        public ProjectConfigurationException(string message, Exception inner)

            : base(message, inner) { }
    }
}

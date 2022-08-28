using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class ServerInfoException : Exception
    {
        public ServerInfoException()
        {
        }

        public ServerInfoException(string? message) : base(message)
        {
        }

        public ServerInfoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ServerInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

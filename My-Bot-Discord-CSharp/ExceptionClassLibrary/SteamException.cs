using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class SteamException : Exception
    {
        public SteamException()
        {
        }

        public SteamException(string? message) : base(message)
        {
        }

        public SteamException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SteamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

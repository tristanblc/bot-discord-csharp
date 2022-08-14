using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class TwitchAPIException : Exception
    {
        public TwitchAPIException()
        {
        }

        public TwitchAPIException(string? message) : base(message)
        {
        }

        public TwitchAPIException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TwitchAPIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

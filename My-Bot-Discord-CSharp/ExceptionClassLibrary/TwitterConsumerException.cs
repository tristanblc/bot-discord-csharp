using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class TwitterException : Exception
    {
        public TwitterException()
        {
        }

        public TwitterException(string? message) : base(message)
        {
        }

        public TwitterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TwitterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

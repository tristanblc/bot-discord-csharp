using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class TweetException : Exception
    {
        public TweetException()
        {
        }

        public TweetException(string? message) : base(message)
        {
        }

        public TweetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TweetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class RedditException : Exception
    {
        public RedditException()
        {
        }

        public RedditException(string? message) : base(message)
        {
        }

        public RedditException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RedditException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

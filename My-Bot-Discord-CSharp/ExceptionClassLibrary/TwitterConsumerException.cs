using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class TwitterConsumerException : Exception
    {
        public TwitterConsumerException()
        {
        }

        public TwitterConsumerException(string? message) : base(message)
        {
        }

        public TwitterConsumerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TwitterConsumerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

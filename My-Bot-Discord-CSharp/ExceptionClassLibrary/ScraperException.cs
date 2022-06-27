using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{

    public class ScraperException : Exception
    {
        public ScraperException()
        {
        }

        public ScraperException(string? message) : base(message)
        {
        }

        public ScraperException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ScraperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

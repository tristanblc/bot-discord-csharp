using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class HelpFormatterException : Exception
    {
        public HelpFormatterException()
        {
        }

        public HelpFormatterException(string? message) : base(message)
        {
        }

        public HelpFormatterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HelpFormatterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class DescriptionRessourceException : Exception
    {
        public DescriptionRessourceException()
        {
        }

        public DescriptionRessourceException(string? message) : base(message)
        {
        }

        public DescriptionRessourceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DescriptionRessourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

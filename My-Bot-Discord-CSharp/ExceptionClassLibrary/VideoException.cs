using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class VideoException : Exception
    {
        public VideoException() { }

        public VideoException(string message)
            : base(message) { }

        public VideoException(string message, Exception inner)
            : base(message, inner) { }
    }
}

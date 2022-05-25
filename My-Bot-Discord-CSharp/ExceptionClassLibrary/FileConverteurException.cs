using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public  class FileConverteurException : Exception
    {
        public FileConverteurException() { }

        public FileConverteurException(string message)
            : base(message) { }

        public FileConverteurException(string message, Exception inner)
            : base(message, inner) { }
    }
}

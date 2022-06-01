using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionClassLibrary
{
    public class FileDownloadException : Exception 
    {

        public FileDownloadException() { }

        public FileDownloadException(string message)
            : base(message) { }

        public FileDownloadException(string message, Exception inner)
            : base(message, inner) { }
    }
}

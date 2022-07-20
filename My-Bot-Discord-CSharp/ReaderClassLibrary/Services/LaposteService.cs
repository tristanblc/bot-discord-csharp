using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Services
{
    public class LaposteService : LaposteApiReader, ILaposteApi
    {
        public LaposteService(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}

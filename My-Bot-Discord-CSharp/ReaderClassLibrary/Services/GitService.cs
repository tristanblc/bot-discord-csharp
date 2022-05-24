using BotClassLibrary.Git;
using ReaderClassLibrary.Interfaces;
using ReaderClassLibrary.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Services
{
    public class GitService : GenericApiReader<GitUser>, IGenericInterface<GitUser>
    {
        public GitService(HttpClient httpClient, string uri) : base(httpClient, uri)
        {

        }
    }
}

using BotClassLibrary.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Interfaces
{
    public interface ILaposteApi
    {
        Task<ReturnMessage> Get(string idShip);
    }
   
}

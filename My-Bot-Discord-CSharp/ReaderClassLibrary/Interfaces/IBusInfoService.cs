using BusClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Interfaces
{
    internal interface IBusInfoService<T> where T : BaseEntity
    {

        Task<T> Get();

        Task<IEnumerable<T>> GetAll();
    }
}

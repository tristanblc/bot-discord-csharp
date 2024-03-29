﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderClassLibrary.Interfaces
{
    public interface IGenericInterface<T> where T : class
    {

        Task<T> Get();
        Task<IEnumerable<T>> GetAll();
    }
}

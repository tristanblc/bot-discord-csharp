﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Exceptions
{
    internal class MyLavalinkException : Exception
    {
        public MyLavalinkException() { }

        public MyLavalinkException(string message)
            : base(message) { }

        public MyLavalinkException(string message, Exception inner)

            : base(message, inner) { }
    }
}

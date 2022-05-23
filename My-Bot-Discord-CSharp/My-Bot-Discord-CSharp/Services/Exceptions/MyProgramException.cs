﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Bot_Discord_CSharp.Services.Exceptions
{
    internal class MyProgramException : Exception
    {
        public MyProgramException() { }

        public MyProgramException(string message)
            : base(message) { }

        public MyProgramException(string message, Exception inner)

            : base(message, inner) { }
    }
}

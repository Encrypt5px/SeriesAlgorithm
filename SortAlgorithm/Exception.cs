﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string name) : base(name)
        {

        }
    }
}

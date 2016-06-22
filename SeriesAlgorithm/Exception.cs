using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesAlgorithm
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string name) : base(name)
        {

        }
    }
}

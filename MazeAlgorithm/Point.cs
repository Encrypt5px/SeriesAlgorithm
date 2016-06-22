using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeAlgorithm
{

    /// <summary>
    /// Point class, include x, y
    /// </summary>
    class Point
    {
        public int x;
        public int y;

        #region Constructor
        public Point()
        {

        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        public override string ToString()
        {
            return x + "," + y;
        }
    }
}

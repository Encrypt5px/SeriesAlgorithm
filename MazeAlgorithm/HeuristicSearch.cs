using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeAlgorithm
{
    /// <summary>
    /// Basic Astar square
    /// </summary>
    class Square : Point
    {
        public int G;//G(n) value
        public int H;//H(n) value
        public int F;//F(n) value

        #region Its parent square

        private Square parent;
        public Square Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                F = this.computeF();
                G = this.computeG();
            }
        }

        #endregion

        #region Constructors

        public Square() { }

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="x0">The x coordinate of this square</param>
        /// <param name="y0">The y coordinate of this square</param>
        public Square(int x0, int y0)
        {
            x = x0;
            y = y0;
        }

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="x0">The x coordinate of this square</param>
        /// <param name="y0">The y coordinate of this square</param>
        /// <param name="G0">The G(n) value of this square</param>
        /// <param name="H0">The H(n) value of this square</param>
        /// <param name="_parent"></param>
        public Square(int x0, int y0, int G0, int H0, Square _parent)
        {
            x = x0;
            y = y0;
            G = G0;
            H = H0;
            Parent = _parent;
        }

        #endregion

        public override string ToString()
        {
            return x + "," + y;
        }
    }

    /// <summary>
    /// Suares Extension
    /// </summary>
    static class SquareExtension
    {
        //Pop the min F(n) square
        public static Square popSquareHasMinF(this Dictionary<string, Square> table, out int x, out int y)
        {
            Square square = table.OrderBy(o => o.Value.F).ElementAt(0).Value;

            x = square.x;
            y = square.y;

            table.Remove(square.ToString());
            return square;
        }

        //Add a square to list or update its parent square
        public static void addOrUpdate(this Dictionary<string, Square> table, Square square,  Square parent)
        {
            string key = square.x + "," + square.y;
            if (table.ContainsKey(key))
            {
                if (square.G < table[key].G)
                    table[key].Parent = parent;
            }
            else
            {
                square.Parent = parent;
                table.Add(key, square);
            }
        }
        
        //Get if is contains (x, y) in table
        public static bool containsCoordinate(this HashSet<string> table, int x, int y)
        {
           return table.Contains(x+","+y);
        }

        #region Extension for Class Square computing

        public static Square targetSquare = new Square();

        //Compute G(n) for square
        public static int computeG(this Square square)
        {
            if (square.Parent == null) return 0;
            if (square.x == square.Parent.x || square.y == square.Parent.y) return square.Parent.G + 10;
            else return square.Parent.G + 14;
        }

        //Compute H(n) for square
        public static int computeH(this Square square, Square squareTo)
        {
            return Math.Abs(square.x - squareTo.x) + Math.Abs(square.y - squareTo.y);
        }

        //Compute F(n) for square
        public static int computeF(this Square square)
        {
            return square.G + square.computeH(targetSquare) * 2;
        }

        #endregion

    }

    /// <summary>
    /// Maze Path Finding
    /// </summary>
    public class HeuristicSearch
    {
        #region Public members
        public string waySign = "*";
        public byte roadOfByte = 1;
        public string wallMark = "\u25a0";
        public string reachedMark = "\u25cf";
        public ConsoleColor wallColor = ConsoleColor.Gray;
        #endregion

        #region Private members
        private Dictionary<string, Square> openTable = new Dictionary<string, Square>();
        private HashSet<string> closeTable = new HashSet<string>();

        private byte[,] map;

        private Square beginSquare;
        private Square targetSquare;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="_map">The map array</param>
        /// <param name="xStart">x coordinate of start</param>
        /// <param name="yStart">y coordinate of start</param>
        /// <param name="xEnd">x coordinate of end</param>
        /// <param name="yEnd">y coordinate of end</param>
        public HeuristicSearch(byte[,] _map)
        {
            this.map = _map;
        }

        #endregion

        #region Main Method, Find the way

        public void findWay(int xStart, int yStart, int xTarget, int yTarget)
        {
            //Define variable
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            List<Square> resultPathList = new List<Square>();
            List<Square> aroundSquares;
            Square squareCurrent;
            
            //initilaize begin and end square
            beginSquare = new Square() { x = xStart, y = yStart};
            targetSquare = new Square() { x = xTarget, y = yTarget };
            SquareExtension.targetSquare = targetSquare;

            //Print map
            Console.Clear();
            printMazeMap();
            Console.ForegroundColor = ConsoleColor.Green;
            
            //Start finding
            stopwatch.Start();

            #region Main Algorithm

            openTable.Add(beginSquare.ToString(), beginSquare);
            for (int xCheck = 0, yCheck = 0; openTable.Count > 0;)
            {
                //Pop the square in openTable which has min f(n) value
                squareCurrent = openTable.popSquareHasMinF(out xCheck, out yCheck);

                //Check if the pop one if our target
                if (IsTarget(xCheck, yCheck))
                {
                    while (squareCurrent != null)
                    {
                        resultPathList.Insert(0, squareCurrent);
                        squareCurrent = squareCurrent.Parent;
                    }
                    break;
                }

                //Check around
                if (checkAround(out aroundSquares, xCheck, yCheck))
                {
                    foreach (Square square in aroundSquares)
                    {
                        openTable.addOrUpdate(square, squareCurrent);
                    }
                }

                //Add checked one to closeTable
                closeTable.Add(squareCurrent.ToString());

                stopwatch.Stop();
                Console.SetCursorPosition(squareCurrent.y * 2, squareCurrent.x);
                Console.Write("\u00d7");
                stopwatch.Start();
            }
            stopwatch.Stop();
            #endregion

            //Output result
            if (resultPathList.Count > 1)
            {
                printWay(resultPathList);
                Console.WriteLine("AStar path finding spend " + stopwatch.ElapsedMilliseconds + " milliseconds");
            }
            else
                Console.WriteLine("Error, pathList length is " + resultPathList.Count);
        }
        #endregion
        
        /// <summary>
        /// Get all squares which can go and near need check square
        /// </summary>
        /// <param name="squares"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool checkAround(out List<Square> squares, int x, int y)
        {
            squares = new List<Square>();
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i == x && j == y) continue;
                    if (CanGo(i, j) && !closeTable.containsCoordinate(i, j))
                    {
                        squares.Add(new Square(i, j));
                    }
                }
            }
            return squares.Count > 0;
        }
        
        #region Print serivices
        /// <summary>
        /// Print maze map step by step
        /// </summary>
        /// <param name="squaresList"></param>
        private void printWay(List<Square> squaresList)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string path = "";
            int pathCount = 0;
            foreach (Square square in squaresList)
            {
                path += ("("+square.ToString()+")->");
                Console.SetCursorPosition(square.y * 2 , square.x);
                Console.Write(reachedMark);
                if (++pathCount == 10)
                {
                    path += "\n";
                    pathCount = 0;
                }

                System.Threading.Thread.Sleep(5);
            }
            Console.SetCursorPosition(0,map.GetLength(0));
            Console.ForegroundColor = wallColor;
            Console.WriteLine(path);
        }

        //Output a maze map
        private void printMazeMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] != roadOfByte)
                    {
                        Console.SetCursorPosition(j * 2, i);
                        Console.Write(wallMark);
                    }
                }
            }
        }
        
        #endregion

        #region Check Coordinate

        private bool CanGo(int x, int y)
        {
            if (x + 1 > map.GetLength(0) || y + 1 > map.GetLength(1) || x < 0 || y < 0)
            {
                return false;
            }
            return (map[x, y] == roadOfByte);
        }
        
        private bool IsTarget(int x, int y)
        {
            if (x != targetSquare.x || y != targetSquare.y ) return false;
            return true;
        }

        #endregion
    }
}

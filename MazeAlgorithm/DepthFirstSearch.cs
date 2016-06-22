using System;
using System.Collections.Generic;

namespace MazeAlgorithm
{
    /// <summary>
    /// Position Enum, Contain eight positon
    /// </summary>
    enum Position : byte
    {
        West = 1,
        WestNorth = 2,
        North = 3,
        EastNorth = 4,
        East = 5,
        EastSouth = 6,
        South = 7,
        WestSouth = 8,
    }
    
    /// <summary>
    /// Lattice robot, a robot contain where it is and the next positon
    /// </summary>
    class LatticeRobot : Point
    {
        //Public Members
        public Position nextPosition;

        #region Public Constructor

        public LatticeRobot(int x, int y, Position pos)
        {
            initPoint(x, y, pos);
        }

        #endregion

        #region Public Method : Initialize point data

        public void initPoint(int x, int y, Position pos)
        {
            this.x = x;
            this.y = y;
            this.nextPosition = pos;
        }

        #endregion
    }
    
    /// <summary>
    /// LatticeRobot helper, extension method collection for lattice robot
    /// </summary>
    static class LatticeRobotHelper
    {
        #region Extension for latticeRobot

        /// <summary>
        /// Find next position close to current
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static bool findNextPosition(this LatticeRobot robot)
        {
            if ((int)robot.nextPosition > 7) return false;
            ++robot.nextPosition;
            //Console.WriteLine(robot.nextPosition.ToString());
            return true;
        }

        /// <summary>
        /// Get next point to go base on robot's nextposition
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="p"></param>
        public static void getNextPoint(this LatticeRobot robot, out Point p)
        {
            int x = robot.x, y = robot.y;
            p = new Point(x, y);
            switch (robot.nextPosition)
            {
                case Position.West: { --p.y; } break;
                case Position.WestNorth: { --p.y; --p.x; } break;
                case Position.WestSouth: { --p.y; ++p.x; } break;
                case Position.North: { --p.x; } break;
                case Position.South: { ++p.x; } break;
                case Position.East: { ++p.y; } break;
                case Position.EastNorth: { ++p.y; --p.x; } break;
                case Position.EastSouth: { ++p.y; ++p.x; } break;
                default: break;
            }
        }

        #endregion
    }

    /// <summary>
    /// Maze map, include map data and the operation for map.
    /// </summary>
    class DepthFirstSearch
    {
        #region Private members
        private byte[,] map;
        private byte wallValue = 0;
        private byte roadValue = 1;
        private byte reachedValue = 2;
        private byte unreachableValue = 3;
        private Point targetPoint;
        #endregion

        #region Public members
        public ConsoleColor wallColor = ConsoleColor.Gray;
        public System.Diagnostics.Stopwatch stopwatch;
        public string nowPointMark = "\u25c6";
        public string wallMark = "\u25a0";
        public string reachedMark = "\u25cf";
        public int updateTime = 1;
        #endregion

        #region Constructor
        public DepthFirstSearch(byte[,] map)
        {
            this.map = map;
        }
        #endregion
        
        #region Main algorithm, find Way

        public void findWay(int xStart, int yStart, int xTarget, int yTarget)
        {

            Stack<LatticeRobot> pathStack = new Stack<LatticeRobot>();
            LatticeRobot robot = new LatticeRobot(xStart, yStart, Position.West);
            targetPoint = new Point(xTarget, yTarget);

            //Print the whole map
            Console.Clear();
            printMap();

            Console.ForegroundColor = ConsoleColor.Green;

            stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            setReached(xStart, yStart);
            pathStack.Push(new LatticeRobot(xStart, yStart, robot.nextPosition));

            #region Loop to find way
            
            for (Point nextPoint; pathStack.Count > 0;)
            {
                //Get the next point want to go
                robot.getNextPoint(out nextPoint);

                //If it's a point can go to, push old data to stack, and set it as current
                if (canGo(nextPoint.x, nextPoint.y))
                {
                    pathStack.Push(new LatticeRobot(robot.x, robot.y, robot.nextPosition));
                    setReached(robot.x, robot.y);//Mark point as reached so that won't repeat.
                    drawNowPosition(nextPoint.x, nextPoint.y);

                    robot.initPoint(nextPoint.x, nextPoint.y, Position.West);
                }
                else
                {
                    //If current point can't go, turn to next position
                    if (!robot.findNextPosition())
                    {
                        // If turn fail, there is no way to go, mark point unreachable and back to last location
                        setUnreachable(robot.x, robot.y);
                        drawUnreachable(robot.x, robot.y);

                        robot = pathStack.Pop();
                        drawNowPosition(robot.x, robot.y);
                    }
                }
                // If robot arrive end point, jump out loop
                if (isTarget(robot.x, robot.y))
                {
                    break;
                }

            }
            stopwatch.Stop();
            Console.ForegroundColor = wallColor;
            Console.SetCursorPosition(0, map.GetLength(0));
            Console.WriteLine("Depth first search path finding spend " + stopwatch.ElapsedMilliseconds + " milliseconds");
            if (pathStack.Count < 1) Console.WriteLine("End with error");

            else printPathStack(pathStack);

            #endregion
        }

        #endregion
        
        #region Point decision

        /// <summary>
        /// Test point(x, y) in map can/can't go
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool canGo(int x, int y)
        {
            if (x + 1 > map.GetLength(0) || y + 1 > map.GetLength(1)) return false;
            if (x < 0 || y < 0) return false;
            if (map[x, y] == roadValue) return true;
            return false;
        }

        private bool canGo(Point p)
        {
            return canGo(p.x, p.y);
        }

        private bool isTarget(int x, int y)
        {
            if (x == targetPoint.x && y == targetPoint.y) return true;
            return false;
        }
        #endregion

        #region Map mark method

        /// <summary>
        /// Mark point(x, y) in map is unreachable
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool setUnreachable(int x, int y)
        {
            if (map[x, y] == unreachableValue || map[x, y] == wallValue) return false;
            map[x, y] = unreachableValue;
            return true;
        }

        /// <summary>
        /// Mark point(x, y) in map has reached
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool setReached(int x, int y)
        {
            if (x < 0 || y < 0) return false;

            drawReached(x, y);

            if (map[x, y] == reachedValue || map[x, y] == wallValue) return false;

            map[x, y] = reachedValue;
            return true;
        }

        #endregion

        #region Print method

        /// <summary>
        /// Output whole map to console
        /// </summary>
        public void printMap()
        {
            Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1) continue;
                    Console.SetCursorPosition(j * 2, i);
                    Console.Write(wallMark);
                }
            }
        }

        /// <summary>
        /// Output solution to console
        /// </summary>
        /// <param name="stack"></param>
        public void printPathStack(Stack<LatticeRobot> stack)
        {
            System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
            int pathCount = 0;
            while (stack.Count != 0)
            {
                LatticeRobot temp = stack.Pop();
                sbResult.Insert(0, "(" + temp.ToString() + ")->");
                if (++pathCount == 10)
                {
                    pathCount = 0;
                    sbResult.Insert(0, "\n");
                }
            }
            Console.WriteLine(sbResult.ToString());
        }

        /// <summary>
        /// Draw point in map is current operate point
        /// </summary>
        /// <param name="action"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void drawNowPosition(int i, int j)
        {
            stopwatch.Stop();
            Console.SetCursorPosition(j * 2, i);
            Console.Write(nowPointMark);
            System.Threading.Thread.Sleep(updateTime);
            stopwatch.Start();
        }

        /// <summary>
        /// Recovery the point has changed to its origin content
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void drawUnreachable(int i, int j)
        {
            stopwatch.Stop();
            Console.SetCursorPosition(j * 2, i);
            Console.Write("\u00d7");
            stopwatch.Start();
        }

        private void drawReached(int i, int j)
        {
            stopwatch.Stop();
            Console.SetCursorPosition(j * 2, i);
            Console.Write(reachedMark);
            stopwatch.Start();
        }

        #endregion

    }
}

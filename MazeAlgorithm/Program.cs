using System;

namespace MazeAlgorithm
{
    class Program
    {
        static byte wallValue = 0;
        static byte roadValue = 1;

        //Crate maze map
        static void createMap(out byte[,] byteMaze)
        {
            MazeCreate mazecreate = new MazeCreate(85, 85);
            mazecreate.createMaze();
            var maze = mazecreate.getMaze();
            byteMaze = new byte[maze.GetLength(0), maze.GetLength(1)];

            char wall = mazecreate.Wall;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    byteMaze[i, j] = maze[i, j] == wall ? wallValue : roadValue;
                }
            }
            Console.WriteLine("Maze creating spend "+ mazecreate.SpendTime + " milliseconds");
        }
        static void Main(string[] args)
        {
            Console.Title = "Maze map find way algorithm";
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            int xStart = 0, yStart = 0, xTarget = 0, yTarget = 0;
            while (true)
            {
                //General part
                Console.Clear();
                Console.WriteLine("Press any key to create maze map");
                Console.ReadKey();
                byte[,] mazeTobyte;
                createMap(out mazeTobyte);

                #region Find any start to target point
                for (int i = 0; i < mazeTobyte.GetLength(1); i++)
                {
                    if (mazeTobyte[0, i] == roadValue)
                    {
                        yStart = i;
                        break;
                    }
                }
                for (int i = mazeTobyte.GetLength(1) - 1; i > 0; i--)
                {
                    if (mazeTobyte[mazeTobyte.GetLength(0) - 1, i] == roadValue)
                    {
                        yTarget = i;
                        xTarget = mazeTobyte.GetLength(0) - 1;
                        break;
                    }
                }
                Console.WriteLine("Start in " + xStart + "," + yStart);
                Console.WriteLine("Traget in " + xTarget + "," + yTarget);
                #endregion

                //First Path Finding method
                Console.WriteLine("Press any key to heuristicSearch");
                Console.ReadKey();
                HeuristicSearch heuristicSearch = new HeuristicSearch(mazeTobyte);
                heuristicSearch.findWay(xStart, yStart, xTarget, yTarget);

                //Second Path Finding method
                Console.WriteLine("Press any key to use depthFirstSearch");
                Console.ReadKey();
                DepthFirstSearch depthFirstSearch = new DepthFirstSearch(mazeTobyte);
                depthFirstSearch.findWay(xStart, yStart, xTarget, yTarget);
                
                Console.WriteLine("Press any key to next loop");
                Console.ReadKey();
            }
        }
    }

}

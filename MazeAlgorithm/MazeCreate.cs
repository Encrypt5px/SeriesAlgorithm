using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeAlgorithm
{
    class pair<T1, T2>
    {
        public pair(T1 arg1, T2 arg2)
        {
            first = arg1;
            second = arg2;
        }
        public T1 first;
        public T2 second;
    }

    class MazeCreate
    {
        public MazeCreate(int rows, int columns)
        {
            maze = new char[rows, columns];
            for (int i = 0; i < maze.GetLength(0); ++i)
            {
                for (int j = 0; j < maze.GetLength(1); ++j)
                {
                    maze[i, j] = wall;
                }
            }
        }

        private char wall = '#';
        public char Wall
        {
            get { return wall; }
            set
            {
                char oldwall = wall;
                wall = value;
                for (int i = 0; i < maze.GetLength(0); ++i)
                {
                    for (int j = 0; j < maze.GetLength(1); ++j)
                    {
                        if (maze[i, j] == oldwall)
                        {
                            maze[i, j] = wall;
                        }
                    }
                }
            }
        }

        private char road = '.';
        public char Road
        {
            get { return road; }
            set
            {
                char oldRoad = road;
                road = value;
                for (int i = 0; i < maze.GetLength(0); ++i)
                {
                    for (int j = 0; j < maze.GetLength(1); ++j)
                    {
                        if (maze[i, j] == oldRoad)
                        {
                            maze[i, j] = road;
                        }
                    }
                }
            }
        }

        private char[,] maze;
        public char[,] getMaze()
        {
            return maze;
        }

        public long SpendTime
        {
            get;
            private set;
        }

        public void createMaze()
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            int x = random.Next(maze.GetLength(0));
            int y = random.Next(maze.GetLength(1));
            List<pair<int, int>> list = new List<pair<int, int>>();

            maze[x, y] = road;
            list.Add(new pair<int, int>(x, y));
            while (list.Count != 0)
            {
                int index = random.Next(list.Count);
                var xy = list[index];
                int newX = xy.first;
                int newY = xy.second;
                if (randomXY(ref newX, ref newY))
                {
                    maze[newX, newY] = road;
                    list.Add(new pair<int, int>(newX, newY));
                }
                else
                {
                    list.RemoveAt(index);
                }

            }

            time.Stop();
            SpendTime = time.ElapsedMilliseconds;
        }
        private bool randomXY(ref int x, ref int y)
        {
            List<pair<int, int>> temp = new List<pair<int, int>>();
            foreach (var t in pos)
            {
                temp.Add(new pair<int, int>(t.first, t.second));
            }
            while (temp.Count != 0)
            {
                int index = random.Next(temp.Count);
                var xy = temp[index];
                temp.RemoveAt(index);
                int newX = x + xy.first;
                int newY = y + xy.second;
                if (get(newX, newY) == wall && aroundCharCount(newX, newY) == 1)
                {
                    x += xy.first;
                    y += xy.second;
                    return true;
                }
            }
            return false;
        }

        private int aroundCharCount(int x, int y, char c = '.')
        {
            int count = 0;
            foreach (var t in pos)
            {
                if (get(x + t.first, y + t.second) == c)
                {
                    ++count;
                }
            }
            return count;
        }

        private char get(int x, int y)
        {
            if (x < 0 || x >= maze.GetLength(0) || y < 0 || y >= maze.GetLength(1))
            {
                return '*';
            }
            return maze[x, y];
        }
        private pair<int, int>[] pos = new pair<int, int>[8] { new pair<int, int>(1,1)
                                                , new pair<int, int>(1,0), new pair<int, int>(1,-1)
                                                , new pair<int, int>(0,1), new pair<int, int>(0,-1)
                                                , new pair<int, int>(-1,1), new pair<int, int>(-1,-1)
                                                , new pair<int, int>(-1,0)};
        private Random random = new Random();
    }
        
}

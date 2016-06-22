using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Example<T>
    {
        private const int MAXINT = 32767;
        private Node<T>[] nodes;      //顶点数组
        private int[,] matrix;       //邻接矩阵数组 
        
        public int[] Prim()
        {
            int[] lowcost = new int[nodes.Length];   //权值数组 
            int[] closevex = new int[nodes.Length];  //顶点数组 
            int mincost = int.MaxValue;              //小权值 

            //辅助数组初始化       
            for (int i = 1; i < nodes.Length; ++i)
            {
                lowcost[i] = matrix[0, i];
                closevex[i] = 0;
            }

            //某个顶点加入集合U     
            lowcost[0] = 0; closevex[0] = 0;
            for (int i = 0; i < nodes.Length; ++i)
            {
                int k = 1; int j = 1;


                //选取权值小的边和相应的顶点    
                while (j < nodes.Length)
                {
                    if (lowcost[j] < mincost && lowcost[j] != 0)
                    {
                        k = j;
                    }
                    ++j;
                }

                //新顶点加入集合U     
                lowcost[k] = 0;

                //重新计算该顶点到其余顶点的边的权值    
                for (j = 1; j < nodes.Length; ++j)
                {
                    if (matrix[k, j] < lowcost[j])
                    {
                        lowcost[j] = matrix[k, j];
                        closevex[j] = k;
                    }
                }
            }

            return closevex;
        }
    }

}

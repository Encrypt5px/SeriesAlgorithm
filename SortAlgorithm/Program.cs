using System;

namespace SortAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            list.push(new int[] { 1,6,8,2,3,9,4,5,0,10});
            list.print();
            Console.WriteLine();

            Pair<LinkedList<int>, LinkedList<int>> pair = list.splite();
            pair.first.print();
            pair.second.print();

            LinkedList<int> newList = combined(pair.first, pair.second);
            newList.print();
            Console.WriteLine();

            sort(newList).print();
            Console.ReadKey();
        }
        /// <summary>
        /// Sort linkedList by loop to splite and combined
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static LinkedList<int> sort(LinkedList<int> list)
        {
            if (list.size() <= 1)
            {
                return list;
            }

            Pair<LinkedList<int>, LinkedList<int>> pair = list.splite();//Splite linkedlist
            Console.WriteLine("\nsplite");
            pair.first.print();
            pair.second.print();
            Console.WriteLine();
            return combined(sort(pair.first), sort(pair.second));//Combine linkedlist
        }
        
        /// <summary>
        /// Combined and sort two linkedList
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <returns></returns>
        static LinkedList<int> combined(LinkedList<int> firstList, LinkedList<int> secondList)
        {
            LinkedList<int> resultList = new LinkedList<int>();
            {
                int firstListSize = firstList.size(), secondListSize = secondList.size();
                int i1 = 0, i2 = 0;
                while (i1 < firstListSize && i2 < secondListSize)
                {
                    int tmpValueInList1 = firstList.at(i1), tmpValueInList2 = secondList.at(i2);
                    if (tmpValueInList1 < tmpValueInList2)
                    {
                        resultList.push(tmpValueInList1);
                        ++i1;
                    }
                    else if (tmpValueInList1 == tmpValueInList2)
                    {
                        resultList.push(tmpValueInList1);
                        resultList.push(tmpValueInList2);
                        ++i1;
                        ++i2;
                    }
                    else
                    {
                        resultList.push(tmpValueInList2);
                        ++i2;
                    }
                }
                if (i1 < firstListSize)
                {
                    for (; i1 < firstListSize; ++i1)
                    {
                        resultList.push(firstList.at(i1));
                    }
                }
                else if (i2 < secondListSize)
                {
                    for (; i2 < secondListSize; ++i2)
                    {
                        resultList.push(secondList.at(i2));
                    }
                }
            }
            Console.WriteLine("\nmerge");
            firstList.print();
            secondList.print();

            Console.WriteLine();
            resultList.print();
            Console.WriteLine();

            firstList.clear();
            secondList.clear();
            return resultList;
        }
    }
}

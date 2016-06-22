using System;

namespace SortAlgorithm
{
    class ListNode<T>
    {
        public T data;
        public ListNode<T> next;
        public ListNode<T> front;

        public ListNode(T dataValue)
        {
            data = dataValue;
        }
        public override string ToString()
        {
            return data.ToString();
        }
    }

    /// <summary>
    /// A pair class for saving two T object
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    class Pair<T1, T2>
    {
        public T1 first;
        public T2 second;
        public Pair(T1 arg1, T2 arg2)
        {
            first = arg1;
            second = arg2;
        }
    }

    class LinkedList<T>
    {
        private ListNode<T> firstNode;
        private ListNode<T> lastNode;
        private int count = 0;

        public int size() { return count + 1; }
        public T at(int i)
        {
            if (i < 0 || i > count) return default(T);
            if (i == 0) return firstNode.data;
            
            ListNode<T> tempNode = firstNode;
            for (int j = 0; j < i; ++j)
            {
                tempNode = tempNode.next;
            }
            return tempNode.data;
        }

        #region Push data to linkedList
        public void push(T[] datas)
        {
            foreach (T data in datas)
            {
                push(data);
            }
        }

        public void push(T data)
        {
            push(new ListNode<T>(data));
        }

        public void push(ListNode<T> newNode)
        {
            if (firstNode == null)
            {
                firstNode = newNode;
                lastNode = firstNode;
            }
            else
            {
                lastNode.next = newNode;
                newNode.front = lastNode;
                lastNode = newNode;
                ++count;
            }
        }
        #endregion

        /// <summary>
        /// Clear the linkedList data by empty fistNode and clear countor
        /// </summary>
        public void clear()
        {
            count = 0;
            firstNode = null;
        }

        /// <summary>
        /// Splite current LinkedList to two LinkedList that size are near the same
        /// </summary>
        /// <returns></returns>
        public Pair<LinkedList<T>, LinkedList<T>> splite()
        {
            LinkedList<T> linkedList1 = new LinkedList<T>();
            LinkedList<T> linkedList2 = new LinkedList<T>();

            int firstListCount = count / 2;
            linkedList1.count = firstListCount;
            linkedList2.count = count - firstListCount -1;

            ListNode<T> tempNode = firstNode;
            for (int i = 0; i < firstListCount; ++i)
            {
                tempNode = tempNode.next;
            }

            linkedList1.firstNode = firstNode;
            linkedList1.lastNode = tempNode;
            linkedList2.firstNode = tempNode.next;
            linkedList2.lastNode = lastNode;

            linkedList1.lastNode.next = null;
            linkedList2.firstNode.front = null;

            clear();
            return new Pair<LinkedList<T>, LinkedList<T>>(linkedList1, linkedList2);
        }

        /// <summary>
        /// print all this LinkedList
        /// </summary>
        public void print()
        {
            if (firstNode == null)
            {
                Console.WriteLine("No node in this linked-list.");
                return;
            }
            else
            {
                ListNode<T> temp = firstNode;
                string output = " ";
                do
                {
                    output += temp.ToString() + " ";
                    temp = temp.next;
                }
                while (temp != null);
                output += " | ";
                Console.Write(output);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesAlgorithm
{
    class ListNode
    {
        private object data;
        private ListNode next;

        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        public ListNode Next
        {
            get { return next; }
            set { next = value; }
        }

        public ListNode(object dataValue, ListNode nextNode)
        {
            data = dataValue;
            next = nextNode;
        }

        public ListNode(object dataValue)
            : this(dataValue, null)
        {
        }
    }

    class List
    {
        private ListNode firstNode;
        private ListNode lastNode;
        private string name;

        public List(string listName)
        {
            name = listName;
            firstNode = lastNode = null;
        }

        public List()
            : this("list")
        {
        }

        public void InsertAtFront(object insertItem)
        {
            lock (this)
            {
                if (IsEmpty())
                {
                    firstNode = lastNode = new ListNode(insertItem);
                }
                else
                {
                    firstNode = new ListNode(insertItem, firstNode);
                }
            }
        }

        public void InsertAtBack(object insertItem)
        {
            lock (this)
            {
                if (IsEmpty())
                {
                    firstNode = lastNode = new ListNode(insertItem);
                }
                else
                {
                    lastNode = lastNode.Next = new ListNode(insertItem);
                }
            }
        }

        public object RemoveFromFront()
        {
            lock (this)
            {
                if (IsEmpty())
                    throw new EmptyListException(name);

                object removeItem = firstNode.Data;

                if (firstNode == lastNode)
                {
                    firstNode = lastNode = null;
                }
                else
                {
                    firstNode = firstNode.Next;
                }

                return removeItem;
            }
        }

        public object RemoveFromBack()
        {
            lock (this)
            {
                if (IsEmpty())
                    throw new EmptyListException(name);

                object removeItem = lastNode.Data;

                if (firstNode == lastNode)
                {
                    firstNode = lastNode = null;
                }
                else
                {
                    ListNode currentNode = firstNode;

                    while (currentNode.Next != lastNode)
                    {
                        currentNode = currentNode.Next;
                    }

                    lastNode = currentNode;
                    currentNode.Next = null;
                }

                return removeItem;
            }
        }

        public bool IsEmpty()
        {
            lock (this)
            {
                return firstNode == null;
            }
        }

        public void Print()
        {
            lock (this)
            {
                if (IsEmpty())
                {
                    Console.WriteLine("Empty " + name);
                    return;
                }

                Console.WriteLine("The " + name + " is: ");

                ListNode currentNode = firstNode;

                while (currentNode != null)
                {
                    Console.WriteLine(currentNode.Data + " ");
                    currentNode = currentNode.Next;
                }

                Console.WriteLine("\n");
            }
        }

        public void Find(object data)
        {
            lock (this)
            {
                ListNode currentNode = firstNode;

                while (currentNode != null)
                {
                    if (currentNode.Data == data)
                    {
                        Console.WriteLine("find {0}", data);
                        return;
                    }
                    else
                    {
                        currentNode = currentNode.Next;
                    }
                }
                Console.WriteLine("{0} not found", data);
            }
        }

        public void Del(object Data)
        {
            lock (this)
            {
                if (IsEmpty())
                {
                    Console.WriteLine("LinkedList is empty");
                    return;
                }
                else
                {
                    ListNode currentNode = firstNode;
                    while (currentNode != null)
                    {
                        if (currentNode.Data == Data)
                        {
                            currentNode.Next = currentNode.Next;
                            return;
                        }
                        else
                        {
                            currentNode = currentNode.Next;
                        }
                    }
                    Console.WriteLine("{0},is deleted", Data);
                }
            }

        }
    }
}

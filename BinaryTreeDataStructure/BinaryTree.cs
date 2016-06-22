using System;
using System.Collections.Generic;

namespace BinaryTreeDataStructure
{
    class BinaryTree<T> where T : IComparable<T>
    {
        private TreeNode<T> rootTreeNode;
        public TreeNode<T> RootTreeNode
        {
            get
            {
                return rootTreeNode;
            }
            private set
            {
                rootTreeNode = value;
            }
        }

        public BinaryTree()
        {

        }
        
        /// <summary>
        /// InitTree using a queue to init a tree
        /// </summary>
        /// <param name="aQueue">a queue include node data</param>
        public void initTree(Queue<TreeNode<T>> aQueue)
        {
            Queue<TreeNode<T>> bQueue = new Queue<TreeNode<T>>();
            TreeNode<T> nodeTemp = new TreeNode<T>();
            TreeNode<T> currentNode = aQueue.Dequeue();

            rootTreeNode = currentNode;
            bQueue.Enqueue(currentNode);

            while (!aQueue.isEmpty())
            {
                nodeTemp = bQueue.Dequeue();
                nodeTemp.LeftChild = aQueue.Dequeue();
                nodeTemp.RightChild = aQueue.Count > 0 ? aQueue.Dequeue() : null;

                if (nodeTemp.LeftChild != null)
                    bQueue.Enqueue(nodeTemp.LeftChild);
                if (nodeTemp.RightChild != null)
                    bQueue.Enqueue(nodeTemp.RightChild);
            }
        }

        #region Order insert data

        /// <summary>
        /// Insert data by order way
        /// </summary>
        /// <param name="data">The data going to insert to tree</param>
        public void orderInsert(T data)
        {
            if (IsEmpty()) rootTreeNode = new TreeNode<T>(data);
            else orderInsert(data, rootTreeNode);
        }

        /// <summary>
        /// Insert data by order way
        /// </summary>
        /// <param name="data">The data going to insert to tree</param>
        /// <param name="currentNode">The node start insert</param>
        private void orderInsert(T data, TreeNode<T> currentNode)
        {
            if (currentNode.Data.CompareTo(data) > 0)
            {
                if (currentNode.LeftChild == null) currentNode.LeftChild = new TreeNode<T>(data);
                else orderInsert(data, currentNode.LeftChild);
            }
            else
            {
                if (currentNode.RightChild == null) currentNode.RightChild = new TreeNode<T>(data);
                else orderInsert(data, currentNode.RightChild);
            }
        }
        #endregion

        #region get depth
        public int depth()
        {
            return depth(rootTreeNode);
        }

        public int depth(TreeNode<T> node)
        {
            if (node == null) return 0;
            int leftDepth = depth(node.LeftChild);
            int rightDepth = depth(node.RightChild);
            return (leftDepth > rightDepth ? leftDepth : rightDepth) + 1;
        }
        #endregion

        /// <summary>
        /// Get if it's current tree empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (rootTreeNode == null)
                return true;
            else
                return false;
        }

        #region Print tree method

        /// <summary>
        /// In order way ,from left to root to right
        /// </summary>
        /// <param name="root"></param>
        public void printInOrder(TreeNode<T> root)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Tree is Empty !");
                return;
            }
            if (root != null)
            {
                printInOrder(root.LeftChild);
                Console.Write(root.Data + " ");
                printInOrder(root.RightChild);
            }
        }
        
        /// <summary>
        /// Print root node then to left then to right
        /// </summary>
        /// <param name="root"></param>
        public void printPreOrder(TreeNode<T> root)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Tree is Empty !");
                return;
            }
            if (root != null)
            {
                Console.Write(root.Data + " ");
                printPreOrder(root.LeftChild);
                printPreOrder(root.RightChild);
            }
        }

        /// <summary>
        /// Print LeftChild and rightChild then print root data
        /// </summary>
        /// <param name="root"></param>
        public void printPostOrder(TreeNode<T> root)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Tree is Empty !");
                return;
            }
            if (root != null)
            {
                printPostOrder(root.LeftChild);
                printPostOrder(root.RightChild);
                Console.Write(root.Data + " ");
            }
        }

        /// <summary>
        /// Print tree like a tree structure
        /// </summary>
        public void printWideOrderTree()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            Queue<TreeNode<T>> queueTemp = new Queue<TreeNode<T>>();
            queueTemp.Enqueue(rootTreeNode);
            while (!queueTemp.isEmpty())
            {
                Console.WriteLine();
                queue = queueTemp;
                queueTemp = new Queue<TreeNode<T>>();
                while (!queue.isEmpty())
                {
                    TreeNode<T> node = queue.Dequeue();
                    Console.Write(node.Data + " ");
                    if (node.LeftChild != null)
                    {
                        queueTemp.Enqueue(node.LeftChild);
                    }
                    if (node.RightChild != null)
                    {
                        queueTemp.Enqueue(node.RightChild);
                    }
                }
            }
        }

        /// <summary>
        /// A method to print tree data using all the different print way
        /// </summary>
        public void printOrder()
        {
            Console.WriteLine("This is order output\n1.In order : ");
            printInOrder(rootTreeNode);
            Console.WriteLine("\n2.Pre order : ");
            printPreOrder(rootTreeNode);
            Console.WriteLine("\n3.Post order : ");
            printPostOrder(rootTreeNode);
            Console.Write("\n4.Wide order : ");
            printWideOrderTree();
        }

        #endregion

        #region add node with height

        /// <summary>
        /// Add one new node using height
        /// </summary>
        /// <param name="height"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool addNode(int height, T data)
        {
            return addNode(height, new TreeNode<T>(data));
        }

        public bool addNode(int height, TreeNode<T> newNode)
        {
            if (newNode == null)
            {
                return false;
            }
            if (IsEmpty())
            {
                rootTreeNode = newNode;
                return true;
            }
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            Queue<TreeNode<T>> tempQueue = new Queue<TreeNode<T>>();
            if (height <= 1)
            {
                return false;
            }
            queue.Enqueue(rootTreeNode);

            for (height -= 2; !queue.isEmpty() && height > 0; --height)
            {
                while (!queue.isEmpty())
                {
                    var node = queue.Dequeue();
                    var temp = node.LeftChild;
                    if (temp != null)
                    {
                        tempQueue.Enqueue(temp);
                    }
                    temp = node.RightChild;
                    if (temp != null)
                    {
                        tempQueue.Enqueue(temp);
                    }
                }
                Queue<TreeNode<T>> exchange = queue;
                queue = tempQueue;
                tempQueue = exchange;
            }
            while (!queue.isEmpty())
            {
                var tnode = queue.Dequeue();
                if (tnode.LeftChild == null)
                {
                    tnode.LeftChild = newNode;
                    return true;
                }
                if (tnode.RightChild == null)
                {
                    tnode.RightChild = newNode;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

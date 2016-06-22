//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BinaryTreeDataStructure
//{
//    class BinarySearchTree
//    {
//        private Node<char> rootNode;

//        public BinarySearchTree()
//        {

//        }

//        public void insert(T data)
//        {
//            Node<char> parentNode;

//            Node<char> newNode = new Node<char>(data);

//            if (rootNode == null)
//            {
//                rootNode = newNode;
//            }
//            else
//            {
//                Node<char> currentNode = rootNode;
//                while (true)
//                {
//                    parentNode = currentNode;
//                    if (newNode.Data < currentNode.Data)
//                    {
//                        currentNode = currentNode.LeftChild;
//                        if (currentNode == null)
//                        {
//                            parentNode.LeftChild = newNode;
//                            break;
//                        }
//                    }
//                    else
//                    {
//                        currentNode = currentNode.RightChild;
//                        if (currentNode == null)
//                        {
//                            parentNode.RightChild = newNode;
//                            break;
//                        }
//                    }
//                }
//            }
//        }

//        public void DisplayAll()
//        {
//            InOrder(rootNode);
//        }

//        public void InOrder(Node<char> root)
//        {
//            if (root != null)
//            {
//                InOrder(root.LeftChild);
//                Console.WriteLine(root.Data);
//                InOrder(root.RightChild);
//            }
//        }
//public void insert(T data)
//{
//    TreeNode<T> parentTreeNode;
//    TreeNode<T> newTreeNode = new TreeNode<T>(data);

//    if (rootTreeNode == null)
//    {
//        rootTreeNode = newTreeNode;
//    }
//    else
//    {
//        TreeNode<T> currentTreeNode = rootTreeNode;
//        while (true)
//        {
//            parentTreeNode = currentTreeNode;
//            if (newTreeNode.Data.CompareTo(currentTreeNode.Data) > 0)
//            {
//                currentTreeNode = currentTreeNode.LeftChild;
//                if (currentTreeNode == null)
//                {
//                    parentTreeNode.LeftChild = newTreeNode;
//                    break;
//                }
//            }
//            else
//            {
//                currentTreeNode = currentTreeNode.RightChild;
//                if (currentTreeNode == null)
//                {
//                    parentTreeNode.RightChild = newTreeNode;
//                    break;
//                }
//            }
//        }
//    }
//}
//    }
//}

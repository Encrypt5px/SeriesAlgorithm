using System;
using System.Collections.Generic;


namespace System.Collections.Generic
{
    static class Extension
    {
        public static bool isEmpty<T>(this Queue<T> queue)
        {
            return queue.Count < 1;
        }
    }
}

namespace BinaryTreeDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input data");
            string input = "ebfad.g..c"; // = Console.ReadLine();
            Console.WriteLine(input);

            Queue<TreeNode<char>> queue = new Queue<TreeNode<char>>();
            foreach (char c in input)
            {
                queue.Enqueue(c == '.' ? null : new TreeNode<char>(c));
            }

            BinaryTree<char> binaryTree = new BinaryTree<char>();
            binaryTree.initTree(queue);
            binaryTree.printOrder();
            Console.WriteLine();

            binaryTree.addNode(binaryTree.depth(), 'm');
            Console.WriteLine("\nInsert m : ");
            binaryTree.printOrder();

            Console.ReadKey();
        }

    }
}

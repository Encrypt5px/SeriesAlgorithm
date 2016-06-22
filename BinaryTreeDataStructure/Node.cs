using System;

namespace BinaryTreeDataStructure
{
    class TreeNode<T> where T : IComparable<T>
    {
        public T Data { get; private set; }
        public TreeNode<T> Parent { get; set; }

        #region Child TreeNode

        private TreeNode<T> leftChild;
        public TreeNode<T> LeftChild
        {
            get
            {
                return leftChild;
            }
            set
            {
                if (leftChild != null && leftChild.Parent != null)
                {
                    leftChild.Parent = null;
                }
                leftChild = value;
                if(value != null) leftChild.Parent = this;
            }
        }

        private TreeNode<T> rightChild;
        public TreeNode<T> RightChild
        {
            get
            {
                return rightChild;
            }
            set
            {
                if (rightChild != null && rightChild.Parent != null)
                {
                    rightChild.Parent = null;
                }
                rightChild = value;
                if (value != null) rightChild.Parent = this;
            }
        }

        #endregion

        public TreeNode() { }
        public TreeNode(T  data)
        {
            Data = data;
        }
    }
}

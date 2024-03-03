using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR
{
    internal class BinaryTreeR<T> where T : IComparable<T>
    {
        private BinaryTreeNodeR<T>? root;
        public int Count { get; private set; } = 0;

        // implement an ASL tree to do self balancing.

        public void Traverse()
        {
            Traverse(root);
        }

        private void Traverse(BinaryTreeNodeR<T>? node)
        {
            if (node == null)
                return;

            Traverse(node.Left);
            Console.WriteLine(node.Value.ToString());
            Traverse(node.Right);
        }

        public void Insert(T value)
        {
            root = Insert(root, value);
        }

        private BinaryTreeNodeR<T> Insert(BinaryTreeNodeR<T>? node, T value)
        {
            if (node == null)
                return new BinaryTreeNodeR<T>(value);

            if (node.Value.CompareTo(value) < 0)
                node.Left = Insert(node.Left, value);
            else if (node.Value.CompareTo(value) > 0)
                node.Right = Insert(node.Right, value);
            else // node.Value.CompareTo(value) == 0
                node.Count++; // TODO: Determine if an error should be thrown here. I don't have a real purpose of a count.

            return node;
        }

        public void Delete(T value)
        {
            root = Delete(root, value);
        }

        private BinaryTreeNodeR<T>? Delete(BinaryTreeNodeR<T>? node, T value)
        {
            if (node == null)
                throw new Exception(string.Format("The provided value is not in the tree: {0}", value.ToString()));

            if (node.Value.CompareTo(value) < 0)
                node.Left = Delete(node.Left, value);
            else if (node.Value.CompareTo(value) > 0)
                node.Right = Delete(node.Right, value);
            else // node.Value.CompareTo(value) == 0
            {
                if (node.Left != null && node.Right != null)
                {
                    // do more complicated logic
                    if (node.Right.Left == null)
                    {
                        node.Right.Left = node.Left;
                        node = node.Right;
                    }
                    else
                    {
                        BinaryTreeNodeR<T> replacementNodeParent = node.Right; // right child of node to be deleted
                        BinaryTreeNodeR<T> replacementNode = node.Right.Left; // The node that will replace the node that is being deleted.
                        
                        // iterate until we get to the left most node of the right child of the node that will be deleted.
                        while (replacementNode.Left != null)
                        {
                            replacementNodeParent = replacementNode;
                            replacementNode = replacementNode.Left;
                        }

                        // Make sure that the right branch of the left most node of the replacement node is not lost.
                        // The replacement node's parent will reference it in it's left branch.
                        replacementNodeParent.Left = replacementNode.Right;

                        replacementNode.Right = node.Right;
                        replacementNode.Left = node.Left;
                        node = replacementNode;
                    }
                }   
                else if (node.Left != null)
                {
                    // Then have the left branch replace the parent.
                    node = node.Left;
                }
                else if (node.Right != null)
                {
                    // Then have the right branch replace the parent.
                    node = node.Right;
                }
                else // If the left and right branches are null
                {
                    node = null;
                }
            }

            return node;
        }

        public bool Search(T value)
        {
            return Search(root, value);
        }

        private bool Search(BinaryTreeNodeR<T>? node, T value)
        {
            if (node == null)
                return false;

            int compareValue = node.Value.CompareTo(value);

            if (compareValue == 0)
                return true;
            else if (compareValue < 0)
                return Search(node.Left, value);
            else // compareValue > 0
                return Search(node.Right, value);
        }
    }

    internal sealed class BinaryTreeNodeR<T>
    {
        public T Value { get; set; }
        public BinaryTreeNodeR<T>? Left { get; set; }
        public BinaryTreeNodeR<T>? Right {  get; set; }
        public int Count { get; set; }

        public  BinaryTreeNodeR(T value)
        {
            Value = value;
            Count = 1;
        }
    }
}

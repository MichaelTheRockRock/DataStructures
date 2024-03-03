using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructuresR
{
    internal class BinaryTreeR<T> where T : IComparable<T>
    {
        private BinaryTreeNodeR<T>? root;
        public int Count { get; private set; } = 0;

        // implement an AVL tree to do self balancing.

        public void Traverse()
        {
            Traverse(root);
        }

        private void CalculateAndSetHeight(BinaryTreeNodeR<T> node)
        {
            int lHeight = node.Left?.Height ?? 0;
            int rHeight = node.Right?.Height ?? 0;

            if (lHeight > rHeight)
                node.Height = lHeight + 1;
            else // When rHeight > lHeight or rHeight == lHeight
                node.Height = rHeight + 1;
        }

        private BinaryTreeNodeR<T> RotateLeft(BinaryTreeNodeR<T> node)
        {
            if (node.Right == null)
                throw new NullReferenceException("The right branch of the passed in node is null.");

            BinaryTreeNodeR<T> pivot = node.Right;
            node.Right = pivot.Left;

            pivot.Left = node;
            node.Height--; // node that has been passed in level;

            node = pivot;
            pivot.Height++; // the pivot node has gone up a level;

            return node;
        }

        private BinaryTreeNodeR<T> RotateRight(BinaryTreeNodeR<T> node)
        {
            if (node.Left == null)
                throw new NullReferenceException("The left branch of the passed in node is null.");

            BinaryTreeNodeR<T> pivot = node.Left;

            node.Left = pivot.Right;

            pivot.Right = node;
            node.Height--; // node that has been passed in level;

            node = pivot;
            pivot.Height++; // the pivot node has gone up a level;

            return node;
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
            {
                this.Count++;
                return new BinaryTreeNodeR<T>(value);
            }

            if (node.Value.CompareTo(value) < 0)
                node.Left = Insert(node.Left, value);
            else if (node.Value.CompareTo(value) > 0)
                node.Right = Insert(node.Right, value);
            else // node.Value.CompareTo(value) == 0
                throw new Exception(string.Format("The value {0} has been already been added to the tree.", value.ToString()));

            CalculateAndSetHeight(node);

            // do check for rotation
            int balanceFactor = node.GetBalanceFactor();

            if (balanceFactor > 1)
            {
                // The right branch is heavy and needs to be re-balanced
                
                if (node.Right!.GetBalanceFactor() >= 0) // This is the RightRight case
                {
                    //rotate left
                    node = RotateLeft(node);
                }
                else // node.Right.GetBalanceFactor() < 0; Right Left Case
                {
                    // rotate right branch right;
                    node.Right = RotateRight(node.Right);

                    // rotate left
                    node = RotateLeft(node);
                }
            }
            else if (balanceFactor < -1)
            {
                // The left branch is heavy and needs to be re-balanced

                if (node.Left!.GetBalanceFactor() <= 0)
                {
                    // rotate right
                    node.Left = RotateRight(node);
                }
                else // node.Right.GetBalanceFactor() > 0 Left Rigth Case 
                {
                    // rotate left branch left
                    node.Left = RotateLeft(node.Left);

                    // rotate right
                    node = RotateRight(node);
                }
            }

            return node;
        }

        public void Delete(T value)
        {
            root = Delete(root, value, 0);
        }

        private BinaryTreeNodeR<T>? Delete(BinaryTreeNodeR<T>? node, T value, int height)
        {
            if (node == null)
                throw new Exception(string.Format("The provided value is not in the tree: {0}", value.ToString()));

            if (node.Value.CompareTo(value) < 0)
                node.Left = Delete(node.Left, value, height + 1);
            else if (node.Value.CompareTo(value) > 0)
                node.Right = Delete(node.Right, value, height + 1);
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

                this.Count--;
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
        public int Height { get; set; }

        public  BinaryTreeNodeR(T value)
        {
            Value = value;
            Height = 1;
        }

        public int GetBalanceFactor()
        {
            return Right?.Height ?? 0 + Left?.Height ?? 0;
        }
    }
}

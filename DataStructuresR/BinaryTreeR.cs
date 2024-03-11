using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructuresR
{
    public class BinaryTreeR<T> where T : IComparable<T>
    {
        private BinaryTreeNodeR<T>? root;
        public int Count { get; private set; } = 0;

        #region Private Methods
        private static BinaryTreeNodeR<T> RotateLeft(BinaryTreeNodeR<T> node)
        {
            if (node.Right == null)
                throw new NullReferenceException("The right branch of the passed in node is null.");

            // We pivot around the right child when doing a left rotation.
            BinaryTreeNodeR<T> pivot = node.Right;

            // Do the left rotation
            node.Right = pivot.Left;
            pivot.Left = node;
            node = pivot;

            // Re-calculate the height of the old parent first as it has moved down a level.
            // This must be done first because it is now the child of the pivot and will affect the pivot's height.
            node.Left.CalculateAndSetHeight(); 
            // Re-calculate the height of the pivot as it was moved up a level
            node.CalculateAndSetHeight();

            return node;
        }

        private static BinaryTreeNodeR<T> RotateRight(BinaryTreeNodeR<T> node)
        {
            if (node.Left == null)
                throw new NullReferenceException("The left branch of the passed in node is null.");

            // We pivot around the left child when doing a right rotation.
            BinaryTreeNodeR<T> pivot = node.Left;

            // Do the right rotation
            node.Left = pivot.Right;
            pivot.Right = node;
            node = pivot;

            // Re-calculate the height of the old parent first as it has moved down a level.
            // This must be done first because it is now the child of the pivot and will affect the pivot's height.
            node.Right.CalculateAndSetHeight();
            // Re-calculate the height of the pivot as it was moved up a level
            node.CalculateAndSetHeight();

            return node;
        }

        private static BinaryTreeNodeR<T> BalanceTree(BinaryTreeNodeR<T> node)
        {
            int balanceFactor = node.GetBalanceFactor();

            if (balanceFactor > 1)
            {
                // The right branch is heavy and needs to be re-balanced

                
                if (balanceFactor != 2)
                    throw new Exception(string.Format("ERROR: Expected balance factor to be 2. The balance factor for node {0} is {1}.", node.Value, balanceFactor));

                if (node.Right == null)
                    throw new Exception($"ERROR: The right branch of the node ({node.Value}) is null when the balance factor is greater than 1.");


                if (node.Right.GetBalanceFactor() >= 0) // This is the RightRight case
                {
                    //rotate left
                    node = BinaryTreeR<T>.RotateLeft(node);
                }
                else // node.Right.GetBalanceFactor() < 0; Right Left Case
                {
                    // rotate right branch right;
                    node.Right = BinaryTreeR<T>.RotateRight(node.Right);

                    // rotate left
                    node = BinaryTreeR<T>.RotateLeft(node);
                }
            }
            else if (balanceFactor < -1)
            {
                // The left branch is heavy and needs to be re-balanced

                if (balanceFactor !=-2)
                    throw new Exception(string.Format("ERROR: Expected balance factor to be -2. The balance factor for node {0} is {1}.", node.Value, balanceFactor));

                if (node.Left == null)
                    throw new Exception($"ERROR: The left branch of the node ({node.Value}) is null when the balance factor is less than -1.");

                if (node.Left.GetBalanceFactor() <= 0)
                {
                    // rotate right
                    node.Left = BinaryTreeR<T>.RotateRight(node);
                }
                else // node.Right.GetBalanceFactor() > 0 Left Rigth Case 
                {
                    // rotate left branch left
                    node.Left = BinaryTreeR<T>.RotateLeft(node.Left);

                    // rotate right
                    node = BinaryTreeR<T>.RotateRight(node);
                }
            }

            return node;
        }

        #endregion
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

        public List<T> GetOrderedList()
        {
            List<T> list = new List<T>();

            GetOrderedList(root, list);

            return list;
        }

        private void GetOrderedList(BinaryTreeNodeR<T>? node, List<T> list)
        {
            if (node == null)
                return;

            GetOrderedList(node.Left, list);
            list.Add(node.Value);
            GetOrderedList(node.Right, list);
        }

        public void Insert(T value)
        {
            root = Insert(root, value);
        }

        private BinaryTreeNodeR<T> Insert(BinaryTreeNodeR<T>? node, T value)
        {
            if (node == null)
            {
                node = new BinaryTreeNodeR<T>(value);
                this.Count++;
            }
            else
            {
                int compareValue = node.Value.CompareTo(value);
                if (compareValue > 0)
                    node.Left = Insert(node.Left, value);
                else if (compareValue < 0)
                    node.Right = Insert(node.Right, value);
                else // compareValue == 0
                    throw new Exception(string.Format("The value {0} has been already been added to the tree.", value.ToString()));
            }

            node.CalculateAndSetHeight();

            node = BinaryTreeR<T>.BalanceTree(node);

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

            int compareValue = node.Value.CompareTo(value);

            if (compareValue > 0)
                node.Left = Delete(node.Left, value, height + 1);
            else if (compareValue < 0)
                node.Right = Delete(node.Right, value, height + 1);
            else // compareValue == 0
            {
                // If check passes, then node has a left and right branch
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
                // Give the first check failed and this check passes, then node has a left branch.
                else if (node.Left != null)
                {
                    // Then have the left branch replace the parent.
                    
                    node = node.Left;
                }
                // Give the first check failed and this check passes, then node has a right branch.
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

            if (node != null)
            {
                // Calculate Height of current node 
                node.CalculateAndSetHeight();

                node = BinaryTreeR<T>.BalanceTree(node);
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
            else if (compareValue > 0)
                return Search(node.Left, value);
            else // compareValue < 0
                return Search(node.Right, value);
        }
    }

    internal sealed class BinaryTreeNodeR<T>
    {
        public T Value { get; set; }
        public BinaryTreeNodeR<T>? Parent { get; set; } // Not using parent at this time.
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
            int rHeight = Right?.Height ?? 0;
            int lHeight = Left?.Height ?? 0;

            return rHeight - lHeight;
        }

        public void CalculateAndSetHeight()
        {
            int lHeight = this.Left?.Height ?? 0;
            int rHeight = this.Right?.Height ?? 0;

            if (lHeight > rHeight)
                this.Height = lHeight + 1;
            else // When rHeight > lHeight or rHeight == lHeight
                this.Height = rHeight + 1;
        }
    }
}

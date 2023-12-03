using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR
{
    public class LinkedListR<T> : IEnumerable<T>
    {
        private LLNodeR<T>? Head { get; set; }
        private LLNodeR<T>? Tail { get; set; }
        private int count = 0;
        public int Count { get { return count; } }

        public LinkedListR() { }

        public LinkedListR(T link)
        {
            
            AddToEnd(link);
        }

        public LinkedListR(T[] list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (T item in list)
            {
                AddToEnd(item);
            }
        }

        
        public LinkedListR(IEnumerable<T> list)
        {
            if (list == null) 
                throw new ArgumentNullException(nameof(list));

            foreach(T item in list)
            {
                AddToEnd(item);
            }
        }
        

        public LLNodeR<T>? First { get { return Head; } }

        public LLNodeR<T>? Last { get { return Tail; } }

        public void Add(T data)
        {
            AddToEnd(data);
        }

        public void AddToFront(T data)
        {
            LLNodeR<T> node = new LLNodeR<T>(this, data);

            if (Head == null)
            {
                Head = node;
                Tail = Head;
            }
            else
            {
                Head.previous = node;
                node.next = Head;
                Head = node;
            }

            count++;
        }

        public void AddToEnd(T data)
        {
            LLNodeR<T> node = new LLNodeR<T>(this, data);

            if (Tail == null)
            {
                Head = node;
                Tail = Head;
            }
            else
            {
                node.previous = Tail;
                Tail.next = node;
                Tail = node;
            }

            count++;
        }

        public LLNodeR<T> InsertBefore(LLNodeR<T> existingNode, T newItem)
        {
            ValidateNode(existingNode);

            LLNodeR<T> newNode = new LLNodeR<T>(existingNode.list!, newItem);

            // Set up the new Node's references
            newNode.next = existingNode;
            newNode.previous = existingNode.Previous;

            // Have the previous node before the existingNode point to the new node with the Next property
            if (existingNode.previous != null)
                existingNode.previous.next = newNode;
            // Have the existing node point back to the new node.
            existingNode.previous = newNode;

            if (existingNode ==  Head)
            {
                Head = newNode;
            }

            count++;

            return newNode;
        }

        public LLNodeR<T> InsertAfter(LLNodeR<T> existingNode, T newItem)
        {
            ValidateNode(existingNode);

            LLNodeR<T> newNode = new LLNodeR<T>(this, newItem);

            // Set up the new Node's references
            newNode.next = existingNode.Next;
            newNode.previous = existingNode;

            // Have the next node after the existingNode point to the new node with the Previous property
            if (existingNode.next != null)
                existingNode.next.previous = newNode;

            // Have the existing node point point to the next task
            existingNode.next = newNode;

            if (existingNode == Tail)
            {
                Tail = newNode;
            }

            count++;

            return newNode;
        }

        public bool Contains(T key)
        {
            return Find(Head, key) != null;
        }

        public LLNodeR<T>? Find(T key)
        {
            return Find(Head, key);
        }

        private LLNodeR<T>? Find(LLNodeR<T>? node, T key)
        {
            if (node == null)
                return null;


            if (node.item != null && node.item.Equals(key))
                return node;
            
            if (node.item == null && key == null)
                return node;

            return Find(node.Next, key);
        }

        public bool Remove(T data)
        {
            LLNodeR<T>? node = Find(Head, data);

            if (node != null)
            {

                if (node == Head) // The found node is the head
                {
                    if (Head == Tail)
                    {
                        Head = null;
                        Tail = null;
                    }
                    else
                    {
                        Head = node.next;

                        if (Head != null)
                            Head.previous = null;
                    }

                }
                else if (node == Tail) // The found node is the tail
                {
                    // Don't need to check if Head is equal to Tail because that would be caught by the check for Head.

                    Tail = node.previous;

                    if (Tail != null)
                        Tail.next = null;
                }
                else // The node is not an end node.
                {
                    LLNodeR<T>? prevNode = node.Previous;
                    LLNodeR<T>? nextNode = node.Next;

                    if (prevNode != null)
                    {
                        prevNode.next = nextNode;
                    }
                    
                    if (nextNode != null)
                    {
                        nextNode.previous = prevNode;
                    }   
                }

                node.ClearReferences();

                count--;

                return true;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (Head == null)
                throw new LinkedListR_ListIsEmpty();

            LLNodeR<T>? node = Head;

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Head = node.next;

                if (Head != null)
                    Head.previous = null;
            }

            node.ClearReferences();

            count--;
        }

        public void RemoveLast()
        {
            if (Tail == null)
                throw new LinkedListR_ListIsEmpty();

            LLNodeR<T>? node = Tail;

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Tail = node.previous;

                if (Tail != null)
                    Tail.next = null;
            }

            node.ClearReferences();

            count--;
        }

        private void ValidateNode(LLNodeR<T> node)
        {
            if (node == null)
                throw new NullReferenceException("The existing node is NULL.");

            if (node.list != this)
                throw new LinkedListR_NotInListException();
        }

        public override string ToString()
        {
            string list = "The list of " + typeof(T) + ":" + Environment.NewLine;

            if (Head == null)
            {
                list = list + "****** There are no items in the list *****";
            }
            else
            {
                list = list + PrintList(Head, 0);
            }

            return list;
        }

        private string PrintList(LLNodeR<T>? node, int number)
        {
            if (node == null)
                return Environment.NewLine;

            number++;

            return string.Format("{0}. {1}{2}", number, node.item?.ToString() ?? "NULL", Environment.NewLine) + PrintList(node.Next, number);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorLinkListR(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct EnumeratorLinkListR : IEnumerator<T>, IEnumerator 
        {
            private readonly LinkedListR<T> _list;
            private LLNodeR<T>? _currentNode;
            private T? _currentValue;
            private int _index;



            internal EnumeratorLinkListR(LinkedListR<T> list)
            {
                _list = list;
                _currentNode = list.Head;
                _currentValue = default;                
                _index = 0;
            }

            public T Current => _currentValue!;

            object? IEnumerator.Current
            {
                get
                {

                    if (_index == 0 || _index > _list.count)
                        throw new Exception("Cannot access list with currently set index");

                    return Current;
                }
            }
            

            public void Dispose()
            {
                // No unmanaged resources to dipose of.
            }

            public bool MoveNext()
            {
                
                if (_currentNode == null)
                {
                    _index++;
                    return false;
                }

                _index++;
                _currentValue = _currentNode.item;
                _currentNode = _currentNode.next;

                return true;
            }

            public void Reset()
            {
                _currentNode = _list.Head;
                _index = 0;
            }
        }
    }

    public sealed class LLNodeR<T>
    {
        // Used as a reference to check to see what list a node belongs to.
        internal LinkedListR<T>? list;
        internal LLNodeR<T>? next;
        internal LLNodeR<T>? previous;
        internal T item;

        public LinkedListR<T>? List { get { return list; } }
        public LLNodeR<T>? Next { get { return next; } }
        public LLNodeR<T>? Previous { get { return previous; } }

        public LLNodeR(T item)
        {
            this.item = item;
        }

        public LLNodeR(LinkedListR<T> list, T item)
        {
            this.list = list;
            this.item = item;
        }

        public T Item
        {
            get { return item; }
            set { item = value; }
        }

        internal void ClearReferences()
        {
            list = null;
            next = null;
            previous = null;
        }
    }

    public sealed class LinkedListR_NotInListException : Exception
    {
        public LinkedListR_NotInListException() { }

        public LinkedListR_NotInListException(string message) : base(message) { }

        public LinkedListR_NotInListException(string message, Exception inner) : base(message, inner) { }
    }

    public sealed class LinkedListR_ListIsEmpty : Exception
    {
        public LinkedListR_ListIsEmpty() { }

        public LinkedListR_ListIsEmpty(string message) : base(message) { }

        public LinkedListR_ListIsEmpty(string message, Exception inner) : base(message, inner) { }
    }
}

using System.Collections.Generic;

namespace DataStructuresR
{
    public class StackR<T>
    {
        private StackNodeR<T>? tail;
        private int count = 0;
        public int Count { get { return count; } }

        public StackR()
        {

        }

        public bool IsEmpty()
        {
            if (tail == null)
                return true;

            return false;
        }

        public void Push(T item)
        {
            StackNodeR<T> node = new StackNodeR<T>(item);

            if (IsEmpty())
            {
                tail = node;
                count++;
                return;
            }

            node.prev = tail;
            tail!.next = node;
            tail = node;

            count++;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new Exception();

            return tail!.item;
        }

        public T Pop()
        {

            if (IsEmpty())
                throw new Exception("Tried to pop when list is empty.");

            StackNodeR<T>? node = tail!;

            tail = tail!.prev;

            if (!IsEmpty())
            {
                tail!.next = null;
            }

            count--;

            return node.item;
        }

    }

    public sealed class StackNodeR<T>
    {
        public T item;
        public StackNodeR<T>? prev;
        public StackNodeR<T>? next;

        public StackNodeR(T item)
        {
            if (item == null)
                throw new Exception("The item cannot be null.");

            this.item = item;
        }
    }

}
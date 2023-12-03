using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR
{
    public class QueueR<T>
    {
        private QueueNodeR<T>? head;
        private QueueNodeR<T>? tail;
        private int count;
        public int Count { get { return count; } }

        public QueueR() {
            count = 0;
        }

        public void Push(T item)
        {
            if (item == null)
                throw new Exception("ERROR: cannot put null value in queue");

            QueueNodeR<T> node = new QueueNodeR<T>(item);

            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                node.next = tail;
                tail!.prev = node;
                tail = node;
            }

            count++;
        }

        public bool IsEmpty()
        {
            if (head == null)
                return true;

            return false;
        }

        public T Peak()
        {
            if (IsEmpty())
                throw new Exception("ERROR: Queue is empty.");

            return head!.item;
        }

        public T Pop()
        {
            if (IsEmpty())
                throw new Exception("ERROR: Queue is empty.");

            T item = head!.item;

            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.prev;
                head!.next = null;
            }

            count--;

            return item;
        }

    }

    public sealed class QueueNodeR<T>
    {
        public T item;
        public QueueNodeR<T>? next;
        public QueueNodeR<T>? prev;

        public QueueNodeR(T item) {

            this.item = item;

        }
    }
}

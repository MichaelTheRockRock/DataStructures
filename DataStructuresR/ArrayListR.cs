using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR
{
    public class ArrayListR<T> : IList<T>
    {
        private T[] items;

        // The number of items currently in the array.
        public int Count
        {
            get;
            private set;
        }

        private const int DefaultSize = 8;

        private int size;

        public int Size
        {
            get 
            {
                return size; 
            }

            set
            {
                if (value < size)
                {
                    throw new Exception("Cannot set size to be lower than current size.");
                }

                if (value > 0)
                {
                    T[] newArray = new T[value];

                    // Copy over the existing items to the new array.
                    if (Count > 0)
                    {
                        Array.Copy(items, newArray, Count);
                    }

                    items = newArray;
                }
            }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public ArrayListR()
        {
            items = new T[DefaultSize];
            size = DefaultSize;

            Count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();


                return items[index];
            }

            set
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();

                items[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            Type? type = Nullable.GetUnderlyingType(typeof(T));

            if (type == null && item == null)
            {
                return -1;
            }

            for (int i = 0; i < Count; i++)
            {
                T value = items[i];

                // Type is not nullable.
                if (type == null)
                {
                    if (item != null && item.Equals(value))
                        return i;
                }
                else
                {
                    if (item == null)
                    {
                        if (value == null)
                            return i;
                    }
                    else
                    {
                        if (item.Equals(value))
                            return i;
                    }
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            // If the number of items in the array is equal to the size, then double the size.
            if (Count == Size)
            {
                Size = Size * 2;
            }

            items[Count] = item;
            Count++;
        }

        public void Clear()
        {
            items = new T[DefaultSize];
        }

        public bool Contains(T item)
        {
            int index = IndexOf(item);

            if (index == -1)
                return false;

            return true;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}

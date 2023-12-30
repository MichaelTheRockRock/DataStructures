using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

                size = value;
            }
        }

        private void ResizeIfNecessary()
        {
            if (Count == Size)
            {
                Size = Size * 2;
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
            return new EnumeratorArrayListR(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                T value = items[i];

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

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count - 1)
                throw new IndexOutOfRangeException();

            ResizeIfNecessary();

            //Make a new array
            T[] newList = new T[Size];

            int newCount = 0;

            // Copy all items from list below index
            for (int i = newCount; i < index; i++, newCount++)
            {
                newList[i] = items[i];
            }

            // Add item to the new array

            newList[newCount] = item;
            newCount++;

            // Add all items from index and above to the new array.
            for (int i = index; i < Count; i++, newCount++)
            {
                newList[newCount] = items[i];
            }

            // set list to the new array.
            items = newList;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new IndexOutOfRangeException("index");

            if (index < Count - 1)
            {
                Array.Copy(items, index + 1, items, index, Count - 1 - index);
            }

            items[Count - 1] = default!;
            Count--;
        }

        public void Add(T item)
        {
            ResizeIfNecessary();

            items[Count] = item;
            Count++;
        }

        public void Clear()
        {            
            if (typeof(T).GetInterfaces().Any(x => x.Name.Equals("Disposable")))
            {
                try
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (items[i] != null)
                            ((IDisposable)items[i]!).Dispose();
                    }   
                }
                catch (NotImplementedException)
                {
                    // Will do nothing if the disposable method has not been implemented.
                }
                catch (Exception)
                {
                    throw;
                }
            }

            items = new T[Size];
            Count = 0;
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
            if (array == null) 
                throw new ArgumentNullException("array");

            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");

            if (array.Length - arrayIndex < Count)
                throw new ArgumentOutOfRangeException("The collection is to large to copy to the allocated memory in array.");

            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                {
                    // Remove the item.

                    //make new array
                    if (i < Count - 1)
                    {
                        Array.Copy(items, i + 1, items, i, Count - 1 - i);
                    }

                    items[Count - 1] = default!;
                    Count--;

                    return true;
                }
            }

            return false;
        }

        public class EnumeratorArrayListR : IEnumerator<T>, IEnumerator
        {
            private readonly ArrayListR<T> _list;
            private T? _currentValue;
            private int _index;

            public T Current => _currentValue!;

            object IEnumerator.Current
            {
                get {

                    if (_index == 0 || _index > _list.Count)
                        throw new Exception("Cannot access list with currently set index");

                    return Current!;
                }

            }
                

            internal EnumeratorArrayListR(ArrayListR<T> list)
            {
                _list = list;
                _currentValue = default;
                _index = 0;
            }
            
            public void Dispose()
            {
                if (_currentValue != null)
                {
                    if (_currentValue.GetType().GetInterfaces().Any(x => x.Name.Equals("IDisposable")))
                    {
                        try
                        {
                            ((IDisposable)_currentValue).Dispose();
                        } catch (NotImplementedException)
                        {
                            // Will do nothing if the disposable method has not been implemented.
                        } catch (Exception)
                        {
                            throw;
                        }

                    }
                }
            }

            public bool MoveNext()
            {
                if (_index == _list.Count)
                {
                    _index++;
                    return false;
                }

                _currentValue = _list[_index];
                _index++;

                return true;
            }

            public void Reset()
            {
                _currentValue = default;
                _index = 0;
            }
        }
    }


}

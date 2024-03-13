﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructuresR
{
    internal enum HashOperations { Search, Insert, Delete }

    public class HashTableR<T, V> where T : notnull, IComparable<T>
    {
        public static int MaxCapacity => int.MaxValue;
        private const int DefaultCapacity = 100;
        private HashNodeR<T, V>?[] buckets;
        public int Count { get; private set; }


        private const decimal LoadFactorMax = 0.618M;

        // The hash table needs to be resized if the LoadFactor is greater than or equal to LoadFactorMax.
        // The hash table needs to be resized if the LoadFactor is less than LoadFactorMax/4  
        private decimal LoadFactor
        {
            get
            {
                return Count * 1.0M / buckets.Length;
            }
        }

        #region Constructors
        public HashTableR() 
        { 
            buckets = new HashNodeR<T, V>[DefaultCapacity];
        }
        #endregion Constructors

        #region Private Methods
        private int? FindIndex(T key, HashOperations hashOp)
        {
            int hashCode = key.GetHashCode();
            int index;
            int i = 0; // The probe position

            // constanst used in probing
            int c1 = 1; // Constant 1
            int c2 = 1; // Constant 2

            do
            {
                // TODO: Think about moving calculation here.
                index = (hashCode + c1 * i + c2 * (i * i)) % buckets.Length;

                // Value found at index
                if (buckets[index] != null)
                {
                    // Does the provided key match the key at the index.
                    // If it does, then take the appropriate action according to passed in hash operation.
                    if (buckets[index]!.Key.CompareTo(key) == 0)
                    {
                        if (HashOperations.Insert == hashOp)
                            throw new Exception(string.Format("The key ({0}) already exists in the table.", key.ToString()));
                        else // Search and Delete 
                            return index;
                    }
                }
                else
                {
                    if (HashOperations.Insert == hashOp)
                        return index;
                    else // Search and Delete
                        throw new Exception(string.Format("The key ({0}) does not exist in the table.", key.ToString()));
                }

                i++;

                // an index should be found for a value by the time it is half the length of the table.
            } while (i < buckets.Length / 2);

            return null;
        }

        private void ResizeTable()
        {
            int newLength = buckets.Length * 2;

            HashNodeR<T, V>?[] newBuckets = new HashNodeR<T, V>[newLength];
            HashNodeR<T, V>?[] oldTableReference = buckets;
            buckets = newBuckets;

            int itemsCopied = 0;

            for (int i = 0; i < oldTableReference.Length && itemsCopied < this.Count; i++)
            { 
                if (oldTableReference[i] != null)
                {
                    this.Insert(oldTableReference[i]!.Key, oldTableReference[i]!.Value);

                    itemsCopied++;
                }
            }
        }
        #endregion Private Methods

        public V this[T key]
        {

            get
            {
                int? index = FindIndex(key, HashOperations.Search);

                if (index == null)
                    throw new ArgumentException("Could not find key in the table.", nameof(key));

                HashNodeR<T, V>? node = buckets[index.Value];

                if (node == null)
                    throw new KeyNotFoundException(string.Format("The key value {0} is not in the set of keys.", key));

                return node.Value;
            }

            set
            {
                int? index = FindIndex(key, HashOperations.Search);

                if (index == null)
                    throw new ArgumentException("Could not find key in the table.", nameof(key));

                HashNodeR<T, V>? node = buckets[index.Value];

                node.Value = value;
            }
        }

        public V? Search(T key)
        {
            int? index = FindIndex(key, HashOperations.Search);

            if (!index.HasValue)
                throw new ArgumentException("Could not find key in the table.", nameof(key));

            return buckets[index.Value].Value;
        }

        

        public void Insert(T key, V value)
        {
            // TODO: Code method to resolve collisions
            // Plan to use quadratic probing
            int? index = FindIndex(key, HashOperations.Insert);

            if (index == null)
            {
                ResizeTable();

                index = FindIndex(key, HashOperations.Insert);
            }

            if (index.HasValue)
            {
                buckets[index.Value] = new HashNodeR<T, V>(key, value);
                this.Count++;
            }
            else
            {
                throw new Exception("Unable to calculate a hash.");
            }
        }

        public void Delete(T key)
        {
            int? index = FindIndex(key, HashOperations.Delete);

            if (index == null)
                throw new Exception("Could not find the key in the table");

            buckets[index.Value] = null;
            this.Count--;

            return;
        }

        public bool Contains(T key)
        {
            int? index = FindIndex(key, HashOperations.Search);

            if (index == null) 
                return false;

            HashNodeR<T, V>? node = buckets[index.Value];

            return node == null ? false : true;
        }
    }

    public sealed class HashNodeR<T, V> where T: notnull
    {
        public T Key { get; init; }
        public V Value { get; set; }

        public HashNodeR(T key, V value)
        {
            Key = key;
            Value = value;
        }
    }
}
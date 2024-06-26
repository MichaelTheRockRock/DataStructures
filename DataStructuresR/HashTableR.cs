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

        private int CalculateIndex(int hashCode, int probePosition)
        {
            // constants used in probing
            int c1 = 1; // Constant 1
            int c2 = 1; // Constant 2

            return (hashCode + probePosition * (c1 + c2 * probePosition)) % buckets.Length;
        }

        private int GetProbeLimit()
        {
            return buckets.Length / 2;
        }

        private int? FindNewKeyIndex(T key)
        {
            int hashCode = key.GetHashCode();
            int index;
            int probePosition = 0;

            do
            {
                index = CalculateIndex(hashCode, probePosition);

                if (buckets[index] == null)
                    return index;
                else if (buckets[index]!.Key.CompareTo(key) == 0)
                    throw new Exception(string.Format("The key ({0}) already exists in the table.", key.ToString()));

                probePosition++;

            } while (probePosition < GetProbeLimit());

            // return null to indicate that there is no open index for an ineert and the table needs to be resized.
            return null;
        }

        private int? FindExistingKeyIndex(T key)
        {
            int hashCode = key.GetHashCode();
            int index;
            int probePosition = 0;

            do
            {
                index = CalculateIndex(hashCode, probePosition);

                if (buckets[index] != null)
                    if (buckets[index]!.Key.CompareTo(key) == 0)
                        return index;

                probePosition++;

            } while (probePosition < GetProbeLimit());

            return null;
        }

        // Used to have the ResizeTable method to increase or decrease the size of the table.
        private enum ResizeVelocity { Decrease, Increase }
        
        private void ResizeTable(ResizeVelocity velocity)
        {
            int newLength;

            switch(velocity)
            {
                case ResizeVelocity.Decrease:
                    newLength = buckets.Length / 2;

                    if (newLength < DefaultCapacity)
                        newLength = DefaultCapacity;

                    break;
                case ResizeVelocity.Increase:
                    newLength = buckets.Length * 2;
                    break;
                default:
                    throw new NotImplementedException();
            }    

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
                int? index = FindExistingKeyIndex(key);

                if (index == null)
                    throw new ArgumentException("Could not find key in the table.", nameof(key));

                HashNodeR<T, V>? node = buckets[index.Value];

                if (node == null)
                    throw new KeyNotFoundException(string.Format("The key value {0} is not in the set of keys.", key));

                return node.Value;
            }

            set
            {
                int? index = FindExistingKeyIndex(key);

                if (index == null)
                    throw new ArgumentException("Could not find key in the table.", nameof(key));

                HashNodeR<T, V>? node = buckets[index.Value];

                if (node == null)
                    throw new NullReferenceException(string.Format("Un-unexpected null value was found at the key \"{0}\"", key));

                node.Value = value;
            }
        }

        public V? Search(T key)
        {
            int? index = FindExistingKeyIndex(key);

            if (!index.HasValue)
                throw new ArgumentException("ERROR: Could not find key in the table.", nameof(key));

            if (buckets[index.Value] == null)
                throw new NullReferenceException(string.Format("ERROR: Could not access the value at the index \"{0}\" given the key \"{0}\"", index, key));

            return (buckets[index.Value] ?? throw new Exception("ERROR: ")).Value;
        }

        public void Insert(T key, V value)
        {
            int? index = FindNewKeyIndex(key);

            while (index == null)
            {
                ResizeTable(ResizeVelocity.Increase);

                index = FindNewKeyIndex(key);
            }

            if (!index.HasValue)
                throw new Exception(string.Format("ERROR: Unable to calculate a hash FRO THE KEY \"{0}\".", key));

            buckets[index.Value] = new HashNodeR<T, V>(key, value);
            this.Count++;

            if (LoadFactor >= LoadFactorMax)
                ResizeTable(ResizeVelocity.Increase);
        }

        public void Delete(T key)
        {

            int? index = FindExistingKeyIndex(key);

            if (index == null)
                throw new Exception("ERROR: Could not find the key in the table");

            buckets[index.Value] = null;
            this.Count--;

            if (LoadFactor < LoadFactorMax / 4 && buckets.Length > DefaultCapacity )
                ResizeTable(ResizeVelocity.Decrease);
        }

        public bool Contains(T key)
        {

            int? index = FindExistingKeyIndex(key);

            if (index == null) 
                return false;

            HashNodeR<T, V>? node = buckets[index.Value];

            if (node == null)
                throw new Exception("ERROR: Could not retrieve value when expected.");

            return true;
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

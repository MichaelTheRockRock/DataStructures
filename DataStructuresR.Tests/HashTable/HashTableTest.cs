using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresR;

namespace DataStructuresR.Tests.HashTable
{
    [TestClass]
    public class HashTableTest
    {

        [TestMethod]
        public void InsertAndCheckForKey()
        {
            HashTableR<int, string> table = new HashTableR<int, string>();

            table.Insert(5, "Hello");

            Assert.IsTrue(table.Contains(5));
        }

        [TestMethod]
        public void InsertAndCheckValueAtKey()
        {
            HashTableR<int, string> table = new HashTableR<int, string>();

            table.Insert(5, "Hello");

            Assert.AreEqual<string>("Hello", table[5]);
        }

        [TestMethod]
        public void InsertMultipleValues()
        {
            HashTableR<int, string> table = new HashTableR<int, string>();

            table.Insert(0, "Hello");
            table.Insert(5, "World");
            table.Insert(10, "My");
            table.Insert(15, "Old");
            table.Insert(20, "Friend");
            table.Insert(25, "How");
            table.Insert(30, "Long");
            table.Insert(35, "Has");
            table.Insert(40, "It");
            table.Insert(45, "Been");
        }

    }
}

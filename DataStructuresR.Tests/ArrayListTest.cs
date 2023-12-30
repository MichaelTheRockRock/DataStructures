using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresR;
using DataStructuresR.Tests.Exceptions;

namespace DataStructuresR.Tests
{
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void TestAdd()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            Assert.AreEqual(8, list.Size, "Should be the default of 8.");

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.AreEqual(8, list.Size, "Should be the default of 8.");

            list.Add(512);

            Assert.AreEqual(16, list.Size, "Should be resized to 16.");

            list.Add(1024);

            Assert.AreEqual(16, list.Size, "Should still be 16.");

            Assert.AreEqual(10, list.Count);

            int count = 0;
            int? expectedIndex = null;

            foreach (int item in list)
            {
                switch(item)
                {
                    case 2:
                        expectedIndex = 0;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 4:
                        expectedIndex = 1;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 8:
                        expectedIndex = 2;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 16:
                        expectedIndex = 3;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 32:
                        expectedIndex = 4;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 64:
                        expectedIndex = 5;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 128:
                        expectedIndex = 6;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 256:
                        expectedIndex = 7;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 512:
                        expectedIndex = 8;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    case 1024:
                        expectedIndex = 9;
                        Assert.AreEqual(expectedIndex, count, string.Format("The value %i was not found at the expected index %i.", item, expectedIndex.Value));
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }

        [TestMethod]
        public void TestIndexing()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            Assert.AreEqual(8, list.Size, "Should be the default of 8.");

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.AreEqual(8, list.Count);

            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(4, list[1]);
            Assert.AreEqual(8, list[2]);
            Assert.AreEqual(16, list[3]);
            Assert.AreEqual(32, list[4]);
            Assert.AreEqual(64, list[5]);
            Assert.AreEqual(128, list[6]);
            Assert.AreEqual(256, list[7]);

            list.Add(1024);

            Assert.AreEqual(1024, list[8]);

        }

        [TestMethod]
        public void TestIndexOf()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.AreEqual(5, list.IndexOf(64));

            Assert.AreEqual(-1, list.IndexOf(68));

            ArrayListR<string?> list2 = new ArrayListR<string?>();

            list2.Add("Things");

            Assert.AreEqual(0, list2.IndexOf("Things"));

            Assert.AreEqual(-1, list2.IndexOf(null));

            list2.Add(null);

            Assert.AreEqual(1, list2.IndexOf(null));
        }

        [TestMethod]
        public void TestInsert()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            list.Add(2);
            list.Add(4);
            list.Add(8);

            Assert.ThrowsException<IndexOutOfRangeException>(() => list.Insert(3, 16));

            list.Insert(2, 16);

            Assert.AreEqual(4, list.Count);

            int count = 0;

            foreach(int item in list)
            {
                Console.WriteLine(item);
                switch(item)
                {
                    case 2:
                        Assert.AreEqual(0, count);
                        break;
                    case 4:
                        Assert.AreEqual(1, count);
                        break;
                    case 8:
                        Assert.AreEqual(3, count);
                        break;
                    case 16:
                        Assert.AreEqual(2, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            list.Insert(0, 32);

            Assert.AreEqual(5, list.Count);

            count = 0;

            foreach (int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 2:
                        Assert.AreEqual(1, count);
                        break;
                    case 4:
                        Assert.AreEqual(2, count);
                        break;
                    case 8:
                        Assert.AreEqual(4, count);
                        break;
                    case 16:
                        Assert.AreEqual(3, count);
                        break;
                    case 32:
                        Assert.AreEqual(0, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            list.Insert(2, 64);
            Assert.AreEqual(6, list.Count);

            count = 0;

            foreach (int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 2:
                        Assert.AreEqual(1, count);
                        break;
                    case 4:
                        Assert.AreEqual(3, count);
                        break;
                    case 8:
                        Assert.AreEqual(5, count);
                        break;
                    case 16:
                        Assert.AreEqual(4, count);
                        break;
                    case 32:
                        Assert.AreEqual(0, count);
                        break;
                    case 64:
                        Assert.AreEqual(2, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }

        [TestMethod]
        public void TestRemoveAt()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.ThrowsException<IndexOutOfRangeException>(() => list.RemoveAt(9));

            list.RemoveAt(3);

            Assert.AreEqual(7, list.Count);

            int count = 0;

            foreach(int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 2:
                        Assert.AreEqual(0, count);
                        break;
                    case 4:
                        Assert.AreEqual(1, count);
                        break;
                    case 8:
                        Assert.AreEqual(2, count);
                        break;
                    case 32:
                        Assert.AreEqual(3, count);
                        break;
                    case 64:
                        Assert.AreEqual(4, count);
                        break;
                    case 128:
                        Assert.AreEqual(5, count);
                        break;
                    case 256:
                        Assert.AreEqual(6, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            list.RemoveAt(6);

            Assert.AreEqual(6, list.Count);

            count = 0;

            foreach (int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 2:
                        Assert.AreEqual(0, count);
                        break;
                    case 4:
                        Assert.AreEqual(1, count);
                        break;
                    case 8:
                        Assert.AreEqual(2, count);
                        break;
                    case 32:
                        Assert.AreEqual(3, count);
                        break;
                    case 64:
                        Assert.AreEqual(4, count);
                        break;
                    case 128:
                        Assert.AreEqual(5, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            list.RemoveAt(0);

            Assert.AreEqual(5, list.Count);

            count = 0;

            foreach (int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 4:
                        Assert.AreEqual(0, count);
                        break;
                    case 8:
                        Assert.AreEqual(1, count);
                        break;
                    case 32:
                        Assert.AreEqual(2, count);
                        break;
                    case 64:
                        Assert.AreEqual(3, count);
                        break;
                    case 128:
                        Assert.AreEqual(4, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }
        }

        [TestMethod]
        public void TestClear()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);
            list.Add(512);

            Assert.AreEqual(9, list.Count);
            Assert.AreEqual(16, list.Size);

            list.Clear();

            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(16, list.Size);
        }

        [TestMethod]
        public void TestContains()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            Assert.IsFalse(list.Contains(2));

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.IsTrue(list.Contains(32));
            Assert.IsFalse(list.Contains(555));
        }

        [TestMethod]
        public void TestContainsWithNullableObjects()
        {
            ArrayListR<string?> list = new ArrayListR<string?>();

            Assert.IsFalse(list.Contains("Fizz"));
            Assert.IsFalse(list.Contains(null));

            list.Add("Fizz");
            list.Add("Buzz");
            list.Add("Lizz");

            Assert.IsTrue(list.Contains("Buzz"));
            Assert.IsFalse(list.Contains("Buxx"));
            Assert.IsFalse(list.Contains(null));

            list.Add(null);

            Assert.IsTrue(list.Contains("Buzz"));
            Assert.IsFalse(list.Contains("Buxx"));
            Assert.IsTrue(list.Contains(null));
        }

        [TestMethod]
        public void TestRemove()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            Assert.IsFalse(list.Remove(2));

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            Assert.IsTrue(list.Remove(32));
            Assert.IsFalse(list.Contains(32));
            Assert.AreEqual(7, list.Count);

            Assert.IsFalse(list.Remove(555));

            list.Add(32);
            list.Add(32);

            Assert.AreEqual(9, list.Count);

            Assert.IsTrue(list.Remove(32));
            Assert.IsTrue(list.Contains(32));

            Assert.AreEqual(8, list.Count);

            int count = 0;

            foreach(int item in list)
            {
                if (item == 32)
                    count++;
            }

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestRemoveWithNullable()
        {
            ArrayListR<string?> list = new ArrayListR<string?>();

            Assert.IsFalse(list.Remove("Fizz"));
            Assert.IsFalse(list.Remove(null));

            list.Add("Fizz");
            list.Add("Buzz");
            list.Add("Lizz");

            Assert.IsTrue(list.Remove("Buzz"));
            Assert.IsFalse(list.Contains("Buzz"));
            Assert.AreEqual(2, list.Count);

            Assert.IsFalse(list.Remove("Subb"));
            Assert.IsFalse(list.Remove(null));

            list.Add("Buzz");
            list.Add("Buzz");
            list.Add(null);

            Assert.AreEqual(5, list.Count);

            Assert.IsTrue(list.Contains("Buzz"));
            Assert.IsTrue(list.Contains(null));

            Assert.IsTrue(list.Remove("Buzz"));
            Assert.AreEqual(4, list.Count);
            Assert.IsTrue(list.Remove(null));
            Assert.AreEqual(3, list.Count);

            int buzzCount = 0;
            int nullCount = 0;

            foreach(string? item in list)
            {
                if (item == "Buzz")
                    buzzCount++;

                if (item == null)
                    nullCount++;
            }

            Assert.AreEqual(1, buzzCount);
            Assert.AreEqual(0, nullCount);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            ArrayListR<int> list = new ArrayListR<int>();

            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(16);
            list.Add(32);
            list.Add(64);
            list.Add(128);
            list.Add(256);

            int[]? nullArray = null;

            Assert.ThrowsException<ArgumentNullException>(() => list.CopyTo(nullArray, 0));

            int[] smallArray = new int[4];

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(smallArray, 0));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(smallArray, 4));

            


            int[] normalArray = new int[list.Count];

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(normalArray, list.Count));

            list.CopyTo(normalArray, 0);

            int count = 0;

            foreach (int item in list)
            {
                Console.WriteLine(item);
                switch (item)
                {
                    case 2:
                        Assert.AreEqual(0, count);
                        break;
                    case 4:
                        Assert.AreEqual(1, count);
                        break;
                    case 8:
                        Assert.AreEqual(2, count);
                        break;
                    case 16:
                        Assert.AreEqual(3, count);
                        break;
                    case 32:
                        Assert.AreEqual(4, count);
                        break;
                    case 64:
                        Assert.AreEqual(5, count);
                        break;
                    case 128:
                        Assert.AreEqual(6, count);
                        break;
                    case 256:
                        Assert.AreEqual(7, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            int[] largeArray = new int[12];

            list.CopyTo(largeArray, 4);

            count = 0;

            foreach(int item in largeArray) 
            {
                Console.WriteLine(item);

                switch (item)
                {
                    case 0:
                        if (count != 0 && count != 1 && count != 2 && count != 3)
                            Assert.Fail();
                        break;
                    case 2:
                        Assert.AreEqual(4, count);
                        break;
                    case 4:
                        Assert.AreEqual(5, count);
                        break;
                    case 8:
                        Assert.AreEqual(6, count);
                        break;
                    case 16:
                        Assert.AreEqual(7, count);
                        break;
                    case 32:
                        Assert.AreEqual(8, count);
                        break;
                    case 64:
                        Assert.AreEqual(9, count);
                        break;
                    case 128:
                        Assert.AreEqual(10, count);
                        break;
                    case 256:
                        Assert.AreEqual(11, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            Console.WriteLine("");
            Console.WriteLine("");

            list.CopyTo(largeArray, 0);

            count = 0;

            foreach (int item in largeArray)
            {
                Console.WriteLine(item);

                switch (item)
                {
                    case 2:
                        Assert.AreEqual(0, count);
                        break;
                    case 4:
                        Assert.AreEqual(1, count);
                        break;
                    case 8:
                        Assert.AreEqual(2, count);
                        break;
                    case 16:
                        Assert.AreEqual(3, count);
                        break;
                    case 32:

                        if (count != 4 && count != 8)
                            Assert.Fail();
                        break;
                    case 64:
                        if (count != 5 && count != 9)
                            Assert.Fail();
                        break;
                    case 128:
                        if (count != 6 && count != 10)
                            Assert.Fail();
                        break;
                    case 256:
                        if (count != 7 && count != 11)
                            Assert.Fail();
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }
    }
}

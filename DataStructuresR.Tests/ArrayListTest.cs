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
    }
}

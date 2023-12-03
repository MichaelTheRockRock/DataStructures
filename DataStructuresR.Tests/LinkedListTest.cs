using DataStructuresR;
using DataStructuresR.Tests.Exceptions;
using NuGet.Frameworks;
using System.Diagnostics;

namespace DataStructuresR.Tests
{
    // TODO: Finish Unit Tests
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void TestInitializationWithSingleItem()
        {
            LinkedListR<int> list = new LinkedListR<int>(8);

            Assert.AreEqual(1, list.Count(), "The list should only contain one item.");

            Assert.IsTrue(list.Contains(8), "The list should contain a the number 8.");
        }

        [TestMethod]
        public void TestInitializationWithArray()
        {
            int[] testSet = new int[] { 8, 16, 32 };

            LinkedListR<int> list = new LinkedListR<int>(testSet);

            Assert.AreEqual(3, list.Count(), "The list should contain 3 items.");

            foreach (int item in testSet)
            {
                if (!list.Contains(item))
                {
                    Assert.IsTrue(list.Contains(item),
                        String.Format("This list does not contain {0}.", item));
                }
            }
        }

        [TestMethod]
        public void TestAdd()
        {

            int[] items = new int[] { 8, 16, 32 };

            LinkedListR<int> list = new LinkedListR<int>();

            foreach (int item in items)
            {
                list.Add(item);
            }

            Assert.AreEqual(3, list.Count(), string.Format("There should be {0} items in the list.", items.Count()));

            int count = 0;

            foreach (int item in list)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count, "The first value in the list should be 8.");
                        break;
                    case 16:
                        Assert.AreEqual(1, count, "The second value in the list should be 16.");
                        break;
                    case 32:
                        Assert.AreEqual(2, count, "The third value in the lsit should be 32.");
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }

        [TestMethod]
        public void TestAddToFront()
        {
            int[] items = new int[] { 8, 16, 32 };

            LinkedListR<int> list = new LinkedListR<int>(items);

            list.AddToFront(64);

            Assert.AreEqual(4, list.Count());

            int count = 0;
            foreach (int item in list)
            {

                if (item == 64)
                    Assert.AreEqual(0, count, "The first item in the list should be 64.");

                count++;
            }
        }

        [TestMethod]
        public void TestAddToFrontWithEmptyList()
        {
            LinkedListR<int> list = new LinkedListR<int>();

            list.AddToFront(64);

            Assert.AreEqual(1, list.Count());

            list.Add(8);

            int count = 0;

            foreach (int item in list)
            {
                if (item == 64)
                    Assert.AreEqual(0, count, "64 should be the first item in teh list.");
            }

        }

        [TestMethod]
        public void TestAddToEnd()
        {

            int[] items = new int[] { 8, 16, 32 };


            LinkedListR<int> list = new LinkedListR<int>(items);

            list.AddToEnd(64);

            int count = 0;

            foreach(int item in list)
            {
                if (item == 64)
                    Assert.AreEqual(3, count);

                count++;
            }

        }


        [TestMethod]
        public void TestAddToEndWithEmptyList()
        {
            LinkedListR<int> list = new LinkedListR<int>();

            list.AddToEnd(64);

            list.Add(8);

            int count = 0;

            foreach(int item in list)
            {
                if (item == 64)
                    Assert.AreEqual(0, count);

                if (item == 8)
                    Assert.AreEqual(1, count);

                count++;
            }
        }


        [TestMethod]
        public void TestInsertBefore()
        {
            int[] items = new int[] { 8, 16, 32 };

            LinkedListR<int> list = new LinkedListR<int>(items);

            var node8 = list.Find(8);

            Assert.IsNotNull(node8);

            list.InsertBefore(node8, 4);

            var node32 = list.Find(32);

            Assert.IsNotNull(node32);

            list.InsertBefore(node32, 24);


            int? prevValue = null;

            foreach(int item in list)
            {
                if (item == 8)
                    Assert.AreEqual(4, prevValue);

                if (item == 32)
                    Assert.AreEqual(24, prevValue);

                prevValue = item;
            }

        }

        [TestMethod]
        public void TestInsertBeforeExistingNodeDoesNotExist()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16 });
            LinkedListR<int> list2 = new LinkedListR<int>(new int[] { 32, 64 });

            // Find a node from list2.
            LLNodeR<int> node = list2.Find(32)!;

            // Try to insert into list1 with respect of the node from list2; this should throw a LinkedListR_NotInListException error.
            Assert.ThrowsException<LinkedListR_NotInListException>(() => list1.InsertBefore(node, 4));

            // Try to insert a value before a null node; this should throw a null
            Assert.ThrowsException<NullReferenceException>(() => list1.InsertBefore(null, 4));

        }

        [TestMethod]
        public void TestInsertAfter()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            LLNodeR<int> node8 = list1.Find(8)!;

            Assert.IsNotNull(node8!);

            list1.InsertAfter(node8, 12);

            var node32 = list1.Find(32);

            Assert.IsNotNull(node32!);

            list1.InsertAfter(node32, 64);

            int? preValue = null;

            foreach (var item in list1)
            {
                if (item == 12)
                    Assert.AreEqual(8, preValue);

                if (item == 64)
                    Assert.AreEqual(32, preValue);

                preValue = item;
            }

        }

        [TestMethod]
        public void TestInsertAfterExistingNodeDoesNotExist()
        {


            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16 });
            LinkedListR<int> list2 = new LinkedListR<int>(new int[] { 32, 64 });

            // Find a node from list2.
            LLNodeR<int> node = list2.Find(32)!;

            // Try to insert into list1 with respect of the node from list2; this should throw a LinkedListR_NotInListException error.
            Assert.ThrowsException<LinkedListR_NotInListException>(() => list1.InsertAfter(node, 4));

            // Try to insert a value before a null node; this should throw a null
            Assert.ThrowsException<NullReferenceException>(() => list1.InsertAfter(null, 4));

        }

        [TestMethod]
        public void TestContains()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsTrue(list1.Contains(8));
            Assert.IsTrue(list1.Contains(16));
            Assert.IsTrue(list1.Contains(32));

            Assert.IsFalse(list1.Contains(64));


            // Test nullable items

            LinkedListR<string> list2 = new LinkedListR<string>(new string[] { "Hello", "World", "!", null });

            Assert.IsTrue(list2.Contains("Hello"));
            Assert.IsTrue(list2.Contains("World"));
            Assert.IsTrue(list2.Contains("!"));
            Assert.IsTrue(list2.Contains(null));
            

        }

        [TestMethod]
        public void TestContainsWithEmptyList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            Assert.IsFalse(list1.Contains(5));

            // Test nullable items
            LinkedListR<string> list2 = new LinkedListR<string>();

            Assert.IsFalse(list2.Contains("Hello"));
            Assert.IsFalse(list2.Contains(null));
        }

        [TestMethod]
        public void TestContainsWhereItemIsNotInList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsFalse(list1.Contains(64));

            LinkedListR<string> list2 = new LinkedListR<string>(new string[] { "Hello", "World", "!" });

            Assert.IsFalse(list2.Contains("stuff"));
            Assert.IsFalse(list2.Contains(null));
        }

        [TestMethod]
        public void TestFind()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            var item = list1.Find(8);

            Assert.IsNotNull(item);
            Assert.AreEqual(8, item.Item);

            item = list1.Find(16);

            Assert.IsNotNull(item);
            Assert.AreEqual(16, item.Item);

            item = list1.Find(32);

            Assert.IsNotNull(item);
            Assert.AreEqual(32, item.Item);


            // list with nullable items

            LinkedListR<string> list2 = new LinkedListR<string>(new string[] { "Hello", "World", "!", null });

            var item2 = list2.Find("Hello");

            Assert.IsNotNull(item2);
            Assert.AreEqual("Hello", item2.Item);

            item2 = list2.Find("World");

            Assert.IsNotNull(item2);
            Assert.AreEqual("World", item2.Item);

            item2 = list2.Find("!");

            Assert.IsNotNull(item2);
            Assert.AreEqual("!", item2.Item);

            item2 = list2.Find(null);

            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Item, null);

        }

        [TestMethod]
        public void TestFindWithEmptyList() 
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            var item1 = list1.Find(8);

            Assert.IsNull(item1);

            // list with nulllable items

            LinkedListR<string> list2 = new LinkedListR<string>();

            var item2 = list2.Find("Hello");

            Assert.IsNull(item2);

            item2 = list2.Find(null);

            Assert.IsNull(item2);

        }

        [TestMethod]
        public void TestFindWhereItemIsNotInList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 16, 32, 64 });

            var item1 = list1.Find(8);

            Assert.IsNull(item1);

            // list with nulllable items

            LinkedListR<string> list2 = new LinkedListR<string>(new string[] { "Big", "World", "!" });

            var item2 = list2.Find("Hello");

            Assert.IsNull(item2);

            item2 = list2.Find(null);

            Assert.IsNull(item2);
        }

        [TestMethod]
        public void TestRemoveWithItemAtFrontOfList()
        { 

            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsTrue(list1.Remove(8));

            Assert.AreEqual(2, list1.Count());

            Assert.IsNull(list1.Find(8));

            int count = 0;
            foreach(var item in list1)
            {
                switch(item)
                {
                    case 16:
                        Assert.AreEqual(0, count);
                        break;
                    case 32:
                        Assert.AreEqual(1, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }
        }

        [TestMethod]
        public void TestRemoveWithItemInTheMiddleOfList()
        {

            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsTrue(list1.Remove(16));

            Assert.AreEqual(2, list1.Count());

            Assert.IsNull(list1.Find(16));

            int count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count);
                        break;
                    case 32:
                        Assert.AreEqual(1, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }

        [TestMethod]
        public void TestRemoveWithItemAtTheEndOfList()
        {

            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsTrue(list1.Remove(32));

            Assert.AreEqual(2, list1.Count());

            Assert.IsNull(list1.Find(32));

            int count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count);
                        break;
                    case 16:
                        Assert.AreEqual(1, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

        }

        [TestMethod]
        public void TestRemoveWithEmptyList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            Assert.IsFalse(list1.Remove(8));

            Assert.AreEqual(0, list1.Count());
        }

        [TestMethod]
        public void TestRemoveWhereItemIsNotInList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            Assert.IsNull(list1.Find(64));

            Assert.AreEqual(3, list1.Count());

            int count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count);
                        break;
                    case 16:
                        Assert.AreEqual(1, count);
                        break;
                    case 32:
                        Assert.AreEqual(2, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }
        }

        [TestMethod]
        public void TestRemoveFirst()
        { 
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });

            list1.RemoveFirst();

            Assert.AreEqual(2, list1.Count());

            int count = 0;
            foreach (var item in list1)
            {
                switch(item)
                {
                    case 16:
                        Assert.AreEqual(0, count);
                        break;
                    case 32:
                        Assert.AreEqual(1, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            list1.RemoveFirst();

            Assert.AreEqual(1, list1.Count());

            count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 32:
                        Assert.AreEqual(0, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            list1.RemoveFirst();

            Assert.AreEqual(0, list1.Count());
        }

        [TestMethod]
        public void TestRemoveFirstWithEmptyList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            Assert.ThrowsException<LinkedListR_ListIsEmpty>(() => list1.RemoveFirst());

            Assert.AreEqual(0, list1.Count());
        }

        [TestMethod]
        public void TestRemoveLast()
        {
            LinkedListR<int> list1 = new LinkedListR<int>( new int[] { 8, 16, 32 });

            list1.RemoveLast();

            Assert.AreEqual(2, list1.Count());

            int count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count);
                        break;
                    case 16:
                        Assert.AreEqual(1, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            list1.RemoveLast();

            Assert.AreEqual(1, list1.Count());

            count = 0;
            foreach (var item in list1)
            {
                switch (item)
                {
                    case 8:
                        Assert.AreEqual(0, count);
                        break;
                    default:
                        throw NotInDataSetException<int>.GetException(item);
                }

                count++;
            }

            list1.RemoveLast();

            Assert.AreEqual(0, list1.Count());
        }

        [TestMethod]
        public void TestRemoveLastWithEmptyList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            Assert.ThrowsException<LinkedListR_ListIsEmpty>(() => list1.RemoveLast());

            Assert.AreEqual(0, list1.Count());
        }

        [TestMethod]
        public void TestToString()
        {
            LinkedListR<int> list1 = new LinkedListR<int>(new int[] { 8, 16, 32 });


            Assert.IsTrue(list1.ToString().Length > 0);
        }

        [TestMethod]
        public void TestToStringWithEmptyList()
        {
            LinkedListR<int> list1 = new LinkedListR<int>();

            Trace.WriteLine(list1.ToString());

            Assert.IsTrue(list1.ToString().Contains("****** There are no items in the list *****"));
        }

    }
}
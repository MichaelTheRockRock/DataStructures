using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DataStructuresR;

namespace DataStructuresR.Tests.BinaryTree
{
    [TestClass]
    public class BinaryTreeTest
    {
        private List<int> testSet;
        private List<int> expectedOrdering;
        private List<int> expectedOrderingMinus14;
        //private FileStream expectedLevels;

        public BinaryTreeTest()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string testSetPath = Path.Combine(baseDirectory, @"..\..\..\BinaryTree\TestData\TestSet.txt");

            using (StreamReader ts = new StreamReader(testSetPath))
            {
                testSet = new List<int>();
                string? line;
                int value = 0;

                while ((line = ts.ReadLine()) != null)
                {
                    if (int.TryParse(line, out value))
                        testSet.Add(value);
                    else
                        throw new Exception("ERROR: tried to parse non-integer from \"TestSet.txt\".");
                }

            }

            string expectedOrderPath = Path.Combine(baseDirectory, @"..\..\..\BinaryTree\AnswerKey\ExpectedOrdering.txt");

            using (StreamReader ts = new StreamReader(expectedOrderPath))
            {
                expectedOrdering = new List<int>();
                string? line;
                int value = 0;

                while ((line = ts.ReadLine()) != null)
                {
                    if (int.TryParse(line, out value))
                        expectedOrdering.Add(value);
                    else
                        throw new Exception("ERROR: tried to parse non-integer from \"ExpectedOrdering.txt\".");
                }
            }

            Assert.AreEqual(testSet.Count, expectedOrdering.Count);

            string expectedOrderingMinus14Path = Path.Combine(baseDirectory, @"..\..\..\BinaryTree\AnswerKey\ExpectedOrderingMinues14.txt");

            using (StreamReader ts = new StreamReader(expectedOrderingMinus14Path))
            {
                expectedOrderingMinus14 = new List<int>();
                string? line;
                int value = 0;

                while ((line = ts.ReadLine()) != null)
                {
                    if (int.TryParse(line, out value))
                        expectedOrderingMinus14.Add(value);
                    else
                        throw new Exception("ERROR: tried to parse non-integer from \"ExpectedOrderingMinues14.txt\".");
                }
            }

            Assert.AreEqual(expectedOrderingMinus14.Count, expectedOrdering.Count - 1);

            //expectedLevels = File.Open("ExpectedLevels.txt", FileMode.Open);
        }


        [TestMethod]
        public void TestInsertNoDuplicates()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            Console.WriteLine("Start adding the values in the test set to the tree.");
            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Console.WriteLine();
            Console.WriteLine("Start searching the tree for all of the values in the test set.");
            foreach(int value in testSet)
            {
                Console.WriteLine(value);
                Assert.IsTrue(tree.Search(value));
            }
        }

        [TestMethod]
        public void TestInsertDuplicates()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            Console.WriteLine("Start adding the values in the test set to the tree.");
            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Console.WriteLine();
            Console.WriteLine("Add a duplicate value to the tree to cause an error.");
            Assert.ThrowsException<Exception>(() => tree.Insert(14), "ERROR: Added the number 14 with resulting in a thrown exception. It should have because there is already a 14 in the tree.");
        }

        [TestMethod]
        public void TestOrdering()
        {
            Console.WriteLine("Begin: TestOrdering");
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Console.WriteLine("Tree Node Count: " + tree.Count);

            List<int> treeList = tree.GetOrderedList();

            Assert.AreEqual(expectedOrdering.Count, treeList.Count);

            for (int i = 0; i < treeList.Count; i++)
            {
                Console.WriteLine("Index: {0} | Expected Value: {1} | Tree Value: {2}", i, treeList[i], expectedOrdering[i]);
                Assert.AreEqual(expectedOrdering[i], treeList[i]);
            }

            Console.WriteLine("End: TestOrdering");
        }

        [TestMethod]
        public void TestSearchFindSuccessful()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Assert.IsTrue(tree.Search(14));
        }

        [TestMethod]
        public void TestSearchFailToFind()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Assert.IsFalse(tree.Search(100));
        }

        [TestMethod]
        public void TestDeleteItemInList()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Assert.IsTrue(tree.Search(14));

            tree.Delete(14);

            Assert.IsFalse(tree.Search(14));
        }

        [TestMethod]
        public void TestDeleteItemNotInList()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Assert.IsFalse(tree.Search(100));

            Assert.ThrowsException<Exception>(() => tree.Delete(100), "ERROR: Should have thrown an exception when it tried to delete the non-existing 100 in the tree.");
        }

        [TestMethod]
        public void TestOrderingAfterDlete()
        {
            BinaryTreeR<int> tree = new BinaryTreeR<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Assert.IsTrue(tree.Search(14));

            tree.Delete(14);

            Assert.IsFalse(tree.Search(14));

            List<int> treeList = tree.GetOrderedList();

            Assert.AreEqual(expectedOrderingMinus14.Count, treeList.Count);

            for (int i = 0; i < treeList.Count; i++)
            {
                Console.WriteLine("Index: {0} | Expected Value: {1} | Tree Value: {2}", i, treeList[i], expectedOrdering[i]);
                Assert.AreEqual(expectedOrderingMinus14[i], treeList[i]);
            }
        }

        ~BinaryTreeTest()
        {
            //expectedOrdering.Dispose();
            //expectedLevels.Dispose();
        }
    }
}

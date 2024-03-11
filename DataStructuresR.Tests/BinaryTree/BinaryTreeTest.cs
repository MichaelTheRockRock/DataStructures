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
                        throw new Exception("ERROR: tried to parse non-integer from \"TestSet.txt\".");
                }
            }

            Assert.AreEqual(testSet.Count, expectedOrdering.Count);

            //expectedLevels = File.Open("ExpectedLevels.txt", FileMode.Open);
        }




        [TestMethod]
        public void TestOrdering()
        {
            Console.WriteLine("Begin: TestOrdering");
            BinaryTreeR<int> tree = new BinaryTreeR<int>();
            List<int> treeOrdering = new List<int>();

            foreach (int value in testSet)
            {
                Console.WriteLine(value);
                tree.Insert(value);
            }

            Console.WriteLine("Tree Node Count: " + tree.Count);

            tree.Traverse(treeOrdering);

            Assert.AreEqual(expectedOrdering.Count, treeOrdering.Count);

            for (int i = 0; i < treeOrdering.Count; i++)
            {
                Console.WriteLine("Index: {0} | Expected Value: {1} | Tree Value: {2}", i, treeOrdering[i], expectedOrdering[i]);
                Assert.AreEqual(expectedOrdering[i], treeOrdering[i]);
            }

            Console.WriteLine("End: TestOrdering");
        }

        ~BinaryTreeTest()
        {
            //expectedOrdering.Dispose();
            //expectedLevels.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresR;

namespace DataStructuresR.Tests
{
    // TODO: Finish Unit Tests
    [TestClass]
    public class QueueTest
    {

        [TestMethod]
        public void TestPush()
        {
            QueueR<int> queue = new QueueR<int>();

            queue.Push(8);

            queue.Push(16);

            queue.Push(32);

            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void TestIsEmptyWithEmptyQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            Assert.IsTrue(queue.IsEmpty());
        }

        [TestMethod]
        public void TestIsEmptyWithItemsInQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            queue.Push(8);

            Assert.IsFalse(queue.IsEmpty());
        }

        [TestMethod]
        public void TestPeakWithItemsInQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            queue.Push(8);
            queue.Push(16);

            Assert.AreEqual(2, queue.Count);

            Assert.AreEqual(8, queue.Peak());

            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void TestPeakWithEmptyQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            Assert.AreEqual(0, queue.Count);

            Assert.ThrowsException<Exception>(() => queue.Peak());
        }

        [TestMethod]
        public void TestPopWithItemsInQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            queue.Push(8);
            queue.Push(16);
            queue.Push(32);

            Assert.AreEqual(3, queue.Count);

            Assert.AreEqual(8, queue.Pop());
            Assert.IsFalse(queue.IsEmpty());
            Assert.AreEqual(2, queue.Count);

            Assert.AreEqual(16, queue.Pop());
            Assert.IsFalse(queue.IsEmpty());
            Assert.AreEqual(1, queue.Count);

            Assert.AreEqual(32, queue.Pop());
            Assert.IsTrue(queue.IsEmpty());
            Assert.AreEqual(0, queue.Count);

        }

        [TestMethod]
        public void TestPopWithEmptyQueue()
        {
            QueueR<int> queue = new QueueR<int>();

            Assert.ThrowsException<Exception>(() => queue.Pop());
        }


    }
}

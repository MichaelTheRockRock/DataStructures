using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR.Tests
{
    // TODO: Finish Unit Tests
    [TestClass]
    public class StackTest
    {

        [TestMethod]
        public void TestPush()
        {
            StackR<int> stack = new StackR<int>();

            stack.Push(8);
            stack.Push(16);
            stack.Push(32);

            Assert.AreEqual(3, stack.Count);
        }

        [TestMethod]
        public void TestIsEmptyWithItemsOnStack()
        {
            StackR<int> stack = new StackR<int>();

            stack.Push(8);
            stack.Push(16);
            stack.Push(32);

            Assert.IsFalse(stack.IsEmpty());
        }

        [TestMethod]
        public void TestIsEmptyWithEmptyStack()
        {
            StackR<int> stack = new StackR<int>();

            Assert.IsTrue(stack.IsEmpty());
        }

        [TestMethod]
        public void TestPeekWithItemsOnStack()
        {
            StackR<int> stack = new StackR<int>();

            stack.Push(8);
            stack.Push(16);
            stack.Push(32);

            Assert.AreEqual(32, stack.Peek());
        }

        [TestMethod]
        public void TestPeekWithEmptyStack()
        {
            StackR<int> stack = new StackR<int>();

            Assert.ThrowsException<Exception>(() => stack.Peek());
        }

        [TestMethod]
        public void TestPop()
        {
            StackR<int> stack = new StackR<int>();

            stack.Push(8);
            stack.Push(16);
            stack.Push(32);

            Assert.AreEqual(32, stack.Pop());
            Assert.AreEqual(2, stack.Count);
            Assert.IsFalse(stack.IsEmpty());

            Assert.AreEqual(16, stack.Pop());
            Assert.AreEqual(1, stack.Count);
            Assert.IsFalse(stack.IsEmpty());

            Assert.AreEqual(8, stack.Pop());
            Assert.AreEqual(0, stack.Count);
            Assert.IsTrue(stack.IsEmpty());

            Assert.ThrowsException<Exception>(() => stack.Pop());
        }

    }
}

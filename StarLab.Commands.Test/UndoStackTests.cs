using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="UndoStack"/> class.
    /// </summary>
    [TestClass]
    public class UndoStackTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var stack = new UndoStack();

            Assert.IsNotNull(stack);
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            var receiver = new MockReceiver<int>(5);
            var add = new TestCommand(receiver);
            var stack = new UndoStack();

            add.Execute(5);
            stack.Add(add);

            Assert.AreEqual(1, stack.UndoCount);
            Assert.AreEqual(10, receiver.Arguments);

            stack.Undo();

            Assert.AreEqual(5, receiver.Arguments);
            Assert.AreEqual(0, stack.UndoCount);
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method works correctly when the stack has been partially reverted.
        /// </summary>
        [TestMethod]
        public void TestAddAfterUndo()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            stack.Undo();
            stack.Undo();

            Assert.AreEqual(10, receiver.Arguments);
            Assert.AreEqual(1, stack.UndoCount);

            var add4 = new TestCommand(receiver);
            add4.Execute(4);
            stack.Add(add4);

            Assert.AreEqual(14, receiver.Arguments);
            Assert.AreEqual(2, stack.UndoCount);

            stack.Undo();

            Assert.AreEqual(10, receiver.Arguments);
            Assert.AreEqual(1, stack.UndoCount);

            stack.Undo();

            Assert.AreEqual(5, receiver.Arguments);
            Assert.AreEqual(0, stack.UndoCount);
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method clears the redo stack.
        /// </summary>
        [TestMethod]
        public void TestAddResetsTheRedoStack()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            stack.Undo();
            stack.Undo();

            Assert.AreEqual(2, stack.RedoCount);
            Assert.AreEqual(1, stack.UndoCount);

            var add1 = new TestCommand(receiver);
            add1.Execute(1);
            stack.Add(add1);

            Assert.AreEqual(0, stack.RedoCount);
            Assert.AreEqual(2, stack.UndoCount);
        }

        /// <summary>
        /// Test that the RedoCount property works correctly.
        /// </summary>
        [TestMethod]
        public void TestGetRedoCount()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            Assert.AreEqual(0, stack.RedoCount);

            stack.Undo();

            Assert.AreEqual(1, stack.RedoCount);

            stack.Undo();

            Assert.AreEqual(2, stack.RedoCount);

            stack.Undo();

            Assert.AreEqual(3, stack.RedoCount);

            stack.Redo();

            Assert.AreEqual(2, stack.RedoCount);

            stack.Redo();

            Assert.AreEqual(1, stack.RedoCount);

            stack.Redo();

            Assert.AreEqual(0, stack.RedoCount);
        }

        /// <summary>
        /// Test that the UndoCount property works correctly.
        /// </summary>
        [TestMethod]
        public void TestGetUndoCount()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            Assert.AreEqual(3, stack.UndoCount);

            stack.Undo();

            Assert.AreEqual(2, stack.UndoCount);

            stack.Undo();

            Assert.AreEqual(1, stack.UndoCount);

            stack.Undo();

            Assert.AreEqual(0, stack.UndoCount);

            stack.Redo();

            Assert.AreEqual(1, stack.UndoCount);

            stack.Redo();

            Assert.AreEqual(2, stack.UndoCount);

            stack.Redo();

            Assert.AreEqual(3, stack.UndoCount);
        }

        /// <summary>
        /// Test that the Redo() method works correctly.
        /// </summary>
        [TestMethod]
        public void TestRedo()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            stack.Undo();
            stack.Undo();
            stack.Undo();

            Assert.AreEqual(5, receiver.Arguments);

            stack.Redo();

            Assert.AreEqual(10, receiver.Arguments);

            stack.Redo();

            Assert.AreEqual(12, receiver.Arguments);

            stack.Redo();

            Assert.AreEqual(15, receiver.Arguments);
        }

        /// <summary>
        /// Test that the Redo() method throws an exception when the redo stack is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRedoStackThrowsExceptionWhenEmpty()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            stack.Undo();
            stack.Undo();
            stack.Redo();
            stack.Redo();
            stack.Redo();
        }

        /// <summary>
        /// Test that the Undo() method works correctly.
        /// </summary>
        [TestMethod]
        public void TestUndo()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            var add3 = new TestCommand(receiver);
            add3.Execute(3);
            stack.Add(add3);

            Assert.AreEqual(15, receiver.Arguments);

            stack.Undo();

            Assert.AreEqual(12, receiver.Arguments);

            stack.Undo();

            Assert.AreEqual(10, receiver.Arguments);

            stack.Undo();

            Assert.AreEqual(5, receiver.Arguments);
        }

        /// <summary>
        /// Test that the Undo() method throws an exception when the undo stack is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUndoStackThrowsExceptionWhenEmpty()
        {
            var receiver = new MockReceiver<int>(5);
            var stack = new UndoStack();

            var add5 = new TestCommand(receiver);
            add5.Execute(5);
            stack.Add(add5);

            var add2 = new TestCommand(receiver);
            add2.Execute(2);
            stack.Add(add2);

            stack.Undo();
            stack.Undo();
            stack.Undo();
        }

        /// <summary>
        /// A test class that implements the <see cref="IRevertableCommand"/> interface.
        /// </summary>
        private class TestCommand : ParameterisedCommand<int, MockReceiver<int>>, IRevertableCommand
        {
            private int arguments;

            public TestCommand(MockReceiver<int> receiver)
                : base(receiver) { }

            public override void Execute(int arguments)
            {
                receiver.Test(receiver.Arguments + arguments);
                this.arguments = arguments;
            }

            public override void Execute()
            {
                receiver.Test(receiver.Arguments + arguments);
            }

            public void Redo()
            {
                Execute();
            }

            public void Undo()
            {
                receiver.Test(receiver.Arguments - arguments);
            }
        }
    }
}

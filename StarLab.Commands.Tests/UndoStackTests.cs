﻿namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="UndoStack"/> class.
    /// </summary>
    public class UndoStackTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var stack = new UndoStack();

            Assert.That(stack, Is.Not.Null);
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method works correctly.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var receiver = new MockReceiver<int>(5);
            var add = new TestCommand(receiver);
            var stack = new UndoStack();

            add.Execute(5);
            stack.Add(add);

            Assert.That(stack.UndoCount, Is.EqualTo(1));
            Assert.That(receiver.Arguments, Is.EqualTo(10));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(5));
            Assert.That(stack.UndoCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method works correctly when the stack has been partially reverted.
        /// </summary>
        [Test]
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

            Assert.That(receiver.Arguments, Is.EqualTo(10));
            Assert.That(stack.UndoCount, Is.EqualTo(1));

            var add4 = new TestCommand(receiver);
            add4.Execute(4);
            stack.Add(add4);

            Assert.That(receiver.Arguments, Is.EqualTo(14));
            Assert.That(stack.UndoCount, Is.EqualTo(2));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(10));
            Assert.That(stack.UndoCount, Is.EqualTo(1));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(5));
            Assert.That(stack.UndoCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the Add(IRevertableCommand) method clears the redo stack.
        /// </summary>
        [Test]
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

            Assert.That(stack.RedoCount, Is.EqualTo(2));
            Assert.That(stack.UndoCount, Is.EqualTo(1));

            var add1 = new TestCommand(receiver);
            add1.Execute(1);
            stack.Add(add1);

            Assert.That(stack.RedoCount, Is.EqualTo(0));
            Assert.That(stack.UndoCount, Is.EqualTo(2));
        }

        /// <summary>
        /// Test that the RedoCount property works correctly.
        /// </summary>
        [Test]
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

            Assert.That(stack.RedoCount, Is.EqualTo(0));

            stack.Undo();

            Assert.That(stack.RedoCount, Is.EqualTo(1));

            stack.Undo();

            Assert.That(stack.RedoCount, Is.EqualTo(2));

            stack.Undo();

            Assert.That(stack.RedoCount, Is.EqualTo(3));

            stack.Redo();

            Assert.That(stack.RedoCount, Is.EqualTo(2));

            stack.Redo();

            Assert.That(stack.RedoCount, Is.EqualTo(1));

            stack.Redo();

            Assert.That(stack.RedoCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the UndoCount property works correctly.
        /// </summary>
        [Test]
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

            Assert.That(stack.UndoCount, Is.EqualTo(3));

            stack.Undo();

            Assert.That(stack.UndoCount, Is.EqualTo(2));

            stack.Undo();

            Assert.That(stack.UndoCount, Is.EqualTo(1));

            stack.Undo();

            Assert.That(stack.UndoCount, Is.EqualTo(0));

            stack.Redo();

            Assert.That(stack.UndoCount, Is.EqualTo(1));

            stack.Redo();

            Assert.That(stack.UndoCount, Is.EqualTo(2));

            stack.Redo();

            Assert.That(stack.UndoCount, Is.EqualTo(3));
        }

        /// <summary>
        /// Test that the Redo() method works correctly.
        /// </summary>
        [Test]
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

            Assert.That(receiver.Arguments, Is.EqualTo(5));

            stack.Redo();

            Assert.That(receiver.Arguments, Is.EqualTo(10));

            stack.Redo();

            Assert.That(receiver.Arguments, Is.EqualTo(12));

            stack.Redo();

            Assert.That(receiver.Arguments, Is.EqualTo(15));
        }

        /// <summary>
        /// Test that the Redo() method throws an exception when the redo stack is empty.
        /// </summary>
        [Test]
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

            Assert.Throws<InvalidOperationException>(() => stack.Redo());
        }

        /// <summary>
        /// Test that the Undo() method works correctly.
        /// </summary>
        [Test]
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

            Assert.That(receiver.Arguments, Is.EqualTo(15));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(12));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(10));

            stack.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(5));
        }

        /// <summary>
        /// Test that the Undo() method throws an exception when the undo stack is empty.
        /// </summary>
        [Test]
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

            Assert.Throws<InvalidOperationException>(() => stack.Undo());
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
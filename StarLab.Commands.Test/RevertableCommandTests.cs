using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on a <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> that implements the <see cref="IRevertableCommand"/> interface.
    /// </summary>
    [TestClass]
    public class RevertableCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var command = new TestCommand(new MockReceiver<int>());

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the constructor throws an exception when the receiver argument is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorWithNullArgument()
        {
            var command = new TestCommand(null);
        }

        /// <summary>
        /// Test that the Execute(int) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            var receiver = new MockReceiver<int>();

            var command = new TestCommand(receiver);

            command.Execute(10);

            Assert.IsTrue(receiver.TestCalled);
            Assert.AreEqual(10, receiver.Arguments);
        }

        /// <summary>
        /// Test that the Redo() method works correctly.
        /// </summary>
        [TestMethod]
        public void TestRedo()
        {
            var receiver = new MockReceiver<int>(15);

            var command = new TestCommand(receiver);

            command.Execute(10);
            command.Undo();

            Assert.AreEqual(15, receiver.Arguments);

            command.Redo();

            Assert.AreEqual(25, receiver.Arguments);
        }

        /// <summary>
        /// Test that the Undo() method works correctly.
        /// </summary>
        [TestMethod]
        public void TestUndo()
        {
            var receiver = new MockReceiver<int>(15);

            var command = new TestCommand(receiver);

            command.Execute(10);

            Assert.AreEqual(25, receiver.Arguments);

            command.Undo();

            Assert.AreEqual(15, receiver.Arguments);
        }

        /// <summary>
        /// A test class that implements the abstract <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class and <see cref="IRevertableCommand"/> interface.
        /// The Execute and Redo methods both add the arguments to the receiver while the Undo method subtracts them from the receiver. 
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
                throw new System.NotImplementedException();
            }

            public void Redo()
            {
                receiver.Test(receiver.Arguments + arguments);
            }

            public void Undo()
            {
                receiver.Test(receiver.Arguments - arguments);
            }
        }
    }
}

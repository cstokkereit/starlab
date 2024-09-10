using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Command&lt;TReceiver&gt;"/> class with state.
    /// </summary>
    [TestClass]
    public class StatefulCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var command = new TestCommand(new MockReceiver<string>(), string.Empty);

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the constructor throws an exception when the receiver argument is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorWithNullArgument()
        {
            var command = new TestCommand(null, string.Empty);
        }

        /// <summary>
        /// Test that the Execute() method works correctly.
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            var receiver = new MockReceiver<string>();

            var command = new TestCommand(receiver, "Testing");

            command.Execute();

            Assert.AreEqual("Testing", receiver.Arguments);
            Assert.IsTrue(receiver.TestCalled);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="Command&lt;TReceiver&gt;"/> class with state.
        /// </summary>
        private class TestCommand : Command<MockReceiver<string>>
        {
            private string state;

            public TestCommand(MockReceiver<string> receiver, string state)
                : base(receiver)
            {
                this.state = state;
            }

            public override void Execute()
            {
                receiver.Test(state);
            }
        }
    }
}

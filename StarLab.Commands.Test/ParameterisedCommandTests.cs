using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class.
    /// </summary>
    [TestClass]
    public class ParameterisedCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var command = new TestCommand(new MockReceiver<MockArguments>());

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
        /// Test that the Execute(TArguments) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            var receiver = new MockReceiver<MockArguments>();

            var command = new TestCommand(receiver);

            command.Execute(new MockArguments("U1", "pwd"));

            Assert.AreEqual("U1", receiver.Arguments.UserName);
            Assert.AreEqual("pwd", receiver.Arguments.Password);
            Assert.IsTrue(receiver.TestCalled);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class.
        /// </summary>
        private class TestCommand : ParameterisedCommand<MockArguments, MockReceiver<MockArguments>>
        {
            public TestCommand(MockReceiver<MockArguments> receiver)
                : base(receiver) { }

            public override void Execute(MockArguments arguments)
            {
                receiver.Test(arguments);
            }

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}

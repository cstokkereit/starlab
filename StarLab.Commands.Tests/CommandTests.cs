namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Command{TReceiver}"/> class.
    /// </summary>
    public class CommandTests
    {
        /// <summary>
        /// Test that the <see cref="Command{IReceiver}(IReceiver)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(Substitute.For<IReceiver>());

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Command{IReceiver}(IReceiver)"/> throws an exception when the receiver argument is null.
        /// </summary>
        [Test]
        public void TestConstructorWithNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => { var command = new TestCommand(null); });
        }

        /// <summary>
        /// Test that the <see cref="Command{TReceiver}.Execute()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = Substitute.For<IReceiver>();

            var command = new TestCommand(receiver);

            command.Execute();

            receiver.Received().Test();
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="Command{TReceiver}"/> class.
        /// </summary>
        private class TestCommand : Command<IReceiver>
        {
            public TestCommand(IReceiver receiver)
                : base(receiver) { }

            public override void Execute()
            {
                receiver.Test();
            }
        }
    }
}

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Command{TReceiver}"/> class with state.
    /// </summary>
    public class StatefulCommandTests
    {
        /// <summary>
        /// Test that the <see cref="Command{IReceiver{string}}(IReceiver{string})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(Substitute.For<IReceiver<string>>(), string.Empty);

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Command{IReceiver{string}}(IReceiver{string})"/> constructor throws an exception when the receiver argument is null.
        /// </summary>
        [Test]
        public void TestConstructorWithNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCommand(null, string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="Command{IReceiver{string}}.Execute()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = Substitute.For<IReceiver<string>>();

            var command = new TestCommand(receiver, "Testing");

            command.Execute();

            receiver.Received().Test("Testing");
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="Command{TReceiver}"/> class with state.
        /// </summary>
        private class TestCommand : Command<IReceiver<string>>
        {
            private string state;

            public TestCommand(IReceiver<string> receiver, string state)
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

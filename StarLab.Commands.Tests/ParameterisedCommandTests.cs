namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class.
    /// </summary>
    public class ParameterisedCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(new MockReceiver<MockArguments>());

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the constructor throws an exception when the receiver argument is null.
        /// </summary>
        [Test]
        public void TestConstructorWithNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCommand(null));
        }

        /// <summary>
        /// Test that the Execute(TArguments) method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = new MockReceiver<MockArguments>();

            var command = new TestCommand(receiver);

            command.Execute(new MockArguments("U1", "pwd"));

            Assert.That(receiver.Arguments.UserName, Is.EqualTo("U1"));
            Assert.That(receiver.Arguments.Password, Is.EqualTo("pwd"));
            Assert.That(receiver.TestCalled);
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

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ParameterisedCommand{TArguments, TReceiver}"/> class.
    /// </summary>
    public class ParameterisedCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(Substitute.For<IReceiver<Arguments>>());

            Assert.That(command, Is.Not.Null);
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
            var receiver = Substitute.For<IReceiver<Arguments>>();

            var command = new TestCommand(receiver);

            var arguments = new Arguments();

            command.Execute(arguments);

            receiver.Received().Test(arguments);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="ParameterisedCommand{TArguments, TReceiver}"/> class.
        /// </summary>
        private class TestCommand : ParameterisedCommand<Arguments, IReceiver<Arguments>>
        {
            public TestCommand(IReceiver<Arguments> receiver)
                : base(receiver) { }

            public override void Execute(Arguments arguments)
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

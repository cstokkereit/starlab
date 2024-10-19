namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on a <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> that implements the <see cref="IRevertableCommand"/> interface.
    /// </summary>
    public class RevertableCommandTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(new MockReceiver<int>());

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
        /// Test that the Execute(int) method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = new MockReceiver<int>();

            var command = new TestCommand(receiver);

            command.Execute(10);

            Assert.IsTrue(receiver.TestCalled);
            Assert.That(receiver.Arguments, Is.EqualTo(10));
        }

        /// <summary>
        /// Test that the Redo() method works correctly.
        /// </summary>
        [Test]
        public void TestRedo()
        {
            var receiver = new MockReceiver<int>(15);

            var command = new TestCommand(receiver);

            command.Execute(10);
            command.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(15));

            command.Redo();

            Assert.That(receiver.Arguments, Is.EqualTo(25));
        }

        /// <summary>
        /// Test that the Undo() method works correctly.
        /// </summary>
        [Test]
        public void TestUndo()
        {
            var receiver = new MockReceiver<int>(15);

            var command = new TestCommand(receiver);

            command.Execute(10);

            Assert.That(receiver.Arguments, Is.EqualTo(25));

            command.Undo();

            Assert.That(receiver.Arguments, Is.EqualTo(15));
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
                throw new NotImplementedException();
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

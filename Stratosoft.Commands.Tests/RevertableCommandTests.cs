using System.Diagnostics;

namespace Stratosoft.Commands
{
    /// <summary>
    /// A class for performing unit tests on a <see cref="ParameterisedCommand{TArguments, TReceiver}"/> that implements the <see cref="IRevertableCommand"/> interface.
    /// </summary>
    public class RevertableCommandTests
    {
        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{int}, int}(IReceiver{int})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(Substitute.For<IReceiver<int>>());

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{TReceiver, TArguments}(TReceiver)"/> constructor throws an exception when the receiver argument is null.
        /// </summary>
        [Test]
        public void TestConstructorWithNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCommand(null));
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{int, IReceiver{int}}.Execute(int)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = Substitute.For<IReceiver<int>>();

            var command = new TestCommand(receiver);

            command.Execute(10);

            receiver.Received().Test(10);
        }

        /// <summary>
        /// Test that the <see cref="IRevertableCommand.Redo()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRedo()
        {
            var receiver = Substitute.For<IReceiver<int>>();

            var command = new TestCommand(receiver);

            command.Execute(10);
            command.Undo();
            command.Redo();

            receiver.Received(1).Test(-10);
            receiver.Received(2).Test(10);
        }

        /// <summary>
        /// Test that the <see cref="IRevertableCommand.Undo()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUndo()
        {
            var receiver = Substitute.For<IReceiver<int>>();

            var command = new TestCommand(receiver);

            command.Execute(10);
            command.Undo();

            receiver.Received(1).Test(-10);
            receiver.Received(1).Test(10);
        }

        /// <summary>
        /// A test class that implements the abstract <see cref="ParameterisedCommand{TReceiver, TArguments}"/> class and <see cref="IRevertableCommand"/> interface.
        /// The Execute and Redo methods both add the arguments to the receiver while the Undo method subtracts them from the receiver. 
        /// </summary>
        private class TestCommand : ParameterisedCommand<IReceiver<int>, int>, IRevertableCommand
        {
            private int arguments;

            public TestCommand(IReceiver<int> receiver)
                : base(receiver) { }

            public override void Execute(int arguments)
            {
                Receiver.Test(arguments);

                this.arguments = arguments;
            }

            public override void Execute()
            {
                throw new NotImplementedException();
            }

            public void Redo()
            {
                Receiver.Test(arguments);
            }

            public void Undo()
            {
                Receiver.Test(-arguments);
            }
        }
    }
}

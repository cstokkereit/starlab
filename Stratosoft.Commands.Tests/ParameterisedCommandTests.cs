using System.Diagnostics;

namespace Stratosoft.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ParameterisedCommand{TReceiver, TArguments}"/> class.
    /// </summary>
    public class ParameterisedCommandTests
    {
        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}()"/> constructor works correctly.
        /// </summary>
        public void TestConstructor()
        {
            var command = new TestCommand();

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}(IReceiver{Arguments})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithReceiver()
        {
            var command = new TestCommand(Substitute.For<IReceiver<Arguments>>());

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}(IReceiver{Arguments})"/> constructor throws an exception when the receiver argument is null.
        /// </summary>
        [Test]
        public void TestConstructorWithNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCommand(null));
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}.Execute(Arguments)"/> method works correctly.
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
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}.Execute(IReceiver{Arguments}, Arguments)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateReceiverAndExecute()
        {
            var receiver = Substitute.For<IReceiver<Arguments>>();

            var command = new TestCommand();

            var arguments = new Arguments();

            command.Execute(receiver, arguments);

            receiver.Received().Test(arguments);
        }

        /// <summary>
        /// Test that the <see cref="ParameterisedCommand{IReceiver{Arguments}, Arguments}.Execute(IReceiver{Arguments}, Arguments)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateExistingReceiverAndExecute()
        {
            var receiver1 = Substitute.For<IReceiver<Arguments>>();
            var receiver2 = Substitute.For<IReceiver<Arguments>>();

            var command = new TestCommand(receiver1);

            var arguments = new Arguments();

            command.Execute(receiver2, arguments);

            receiver1.DidNotReceive().Test(arguments);
            receiver2.Received().Test(arguments);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="ParameterisedCommand{TReceiver, TArguments}"/> class.
        /// </summary>
        private class TestCommand : ParameterisedCommand<IReceiver<Arguments>, Arguments>
        {
            public TestCommand(IReceiver<Arguments> receiver)
                : base(receiver) { }

            public TestCommand() { }

            public override void Execute(Arguments arguments)
            {
                Receiver.Test(arguments);
            }

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}

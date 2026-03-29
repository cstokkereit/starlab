namespace Stratosoft.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ActionCommand"/> class.
    /// </summary>
    public class ActionCommandTests
    {
        /// <summary>
        /// Test that the <see cref="ActionCommand{IReceiver}(IReceiver)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new ActionCommand(() => { });

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Command{TReceiver}.Execute()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            var receiver = Substitute.For<IReceiver>();

            var command = new ActionCommand(receiver.Test0);

            command.Execute();

            receiver.Received().Test0();
        }

        /// <summary>
        /// Test that the <see cref="Command{TReceiver}.Execute()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExecuteWithLambda()
        {
            var receiver = Substitute.For<IReceiver>();

            var command = new ActionCommand(() => { 
                receiver.Test1("Testing");
            });

            command.Execute();

            receiver.Received().Test1("Testing");
        }

        /// <summary>
        /// An interface with a method that has the same signature as <see cref="Action"/>.
        /// </summary>
        public interface IReceiver
        {
            void Test0();

            void Test1(string arg);
        }
    }
}

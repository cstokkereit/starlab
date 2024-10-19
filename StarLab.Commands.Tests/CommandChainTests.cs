namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CommandChain"/> class.
    /// </summary>
    public class CommandChainTests
    {

        // TODO - Add tests for ComponentCommand version of AggregateCommand

        public void TestDefaultConstructor()
        {
            var command = new CommandChain();

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the Execute() method works correctly when the <see cref="CommandChain"/> was initialised with a collection.
        /// </summary>
        [Test]
        public void TestExecuteCommandChain()
        {
            var receiver = new TestReceiver();

            var command = new CommandChain();

            command.Add(new TestCommand(receiver, "This"));
            command.Add(new TestCommand(receiver, "is"));
            command.Add(new TestCommand(receiver, "a"));
            command.Add(new TestCommand(receiver, "test."));

            command.Execute();

            Assert.That(receiver.Text, Is.EqualTo("This is a test."));
        }

        /// <summary>
        /// A class used to test the <see cref="CommandChain"/> class.
        /// </summary>
        private class TestReceiver
        {
            public string Text { get; private set; }

            public void AppendText(string text)
            {
                Text = Text + " " + text;
                Text = Text.Trim();
            }
        }

        /// <summary>
        /// A derived class used to test the <see cref="CommandChain"/> class.
        /// </summary>
        private class TestCommand : Command<TestReceiver>
        {
            private readonly string text;

            public TestCommand(TestReceiver receiver, string text)
                : base(receiver)
            {
                this.text = text;
            }

            public override void Execute()
            {
                receiver.AppendText(text);
            }
        }
    }
}

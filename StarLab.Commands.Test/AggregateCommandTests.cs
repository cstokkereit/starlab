namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AggregateCommand"/> class.
    /// </summary>
    [TestClass]
    public class AggregateCommandTests
    {

        // TODO - Add tests for ComponentCommand version of AggregateCommand

        /// <summary>
        /// Test that the AggregateCommand(IEnumerable<ICommand>commands) constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestCollectionConstructor()
        {
            var command = new AggregateCommand(new List<ICommand>());

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the AggregateCommand(params ICommand[] commands) constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestParamArrayConstructor()
        {
            var receiver = new MockReceiver<string>();

            var command = new AggregateCommand(new MockCommand(receiver), new MockCommand(receiver), new MockCommand(receiver));

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the Execute() method works correctly when the <see cref="AggregateCommand"/> was initialised with a collection.
        /// </summary>
        [TestMethod]
        public void TestExecuteWithCollection()
        {
            var receiver = new TestReceiver();

            var commands = new []
            {
                new TestCommand(receiver, "This"),
                new TestCommand(receiver, "is"),
                new TestCommand(receiver, "a"),
                new TestCommand(receiver, "test.")
            };

            var command = new AggregateCommand(commands);

            command.Execute();

            Assert.AreEqual("This is a test.", receiver.Text);
        }

        /// <summary>
        /// Test that the Execute() method works correctly when the <see cref="AggregateCommand"/> was initialised with a param array.
        /// </summary>
        [TestMethod]
        public void TestExecuteWithParamArray()
        {
            var receiver = new TestReceiver();

            var command = new AggregateCommand(new TestCommand(receiver, "This"), new TestCommand(receiver, "is"), new TestCommand(receiver, "a"), new TestCommand(receiver, "test."));

            command.Execute();

            Assert.AreEqual("This is a test.", receiver.Text);
        }

        /// <summary>
        /// A class used to test the <see cref="AggregateCommand"/> class.
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
        /// A derived class used to test the <see cref="AggregateCommand"/> class.
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

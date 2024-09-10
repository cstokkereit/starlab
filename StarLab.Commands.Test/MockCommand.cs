namespace StarLab.Commands
{
    /// <summary>
    /// A mock class used for testing the classes derived from <see cref="CommandInvoker&lt;TComponent&gt;"/> as well as the <see cref="AggregateCommand"/> and <see cref="CommandManager"/> classes. 
    /// </summary>
    class MockCommand : Command<MockReceiver<string>>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MockCommand"/> class.
        /// </summary>
        /// <param name="receiver">The mock receiver to be used in the test.</param>
        public MockCommand(MockReceiver<string> receiver)
            : base(receiver) { }

        /// <summary>
        /// Returns true if the Execute method was called; false otherwise.
        /// </summary>
        public bool ExecuteCalled { get; private set; }

        /// <summary>
        /// A test method that will be called by the class under test.
        /// </summary>
        public override void Execute()
        {
            ExecuteCalled = true;
        }
    }
}

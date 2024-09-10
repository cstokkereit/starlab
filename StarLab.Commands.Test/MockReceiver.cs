namespace StarLab.Commands
{
    /// <summary>
    /// A mock class used for testing the command classes.
    /// </summary>
    /// <typeparam name="TArguments">The type of the arguments that will be used for the test.</typeparam>
    class MockReceiver<TArguments>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MockReceiver&lt;TArguments&gt;"/> class.
        /// </summary>
        /// <param name="arguments">The arguments to be used in the test.</param>
        public MockReceiver(TArguments arguments)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MockReceiver&lt;TArguments&gt;"/> class.
        /// </summary>
        public MockReceiver() { }

        /// <summary>
        /// A test method that requires arguments of the specified type and will be called by the command under test.
        /// </summary>
        /// <param name="arguments">The arguments provided by the command under test.</param>
        public void Test(TArguments arguments)
        {
            Arguments = arguments;
            TestCalled = true;
        }

        /// <summary>
        /// A test method that will be called by the command under test.
        /// </summary>
        public void Test()
        {
            TestCalled = true;
        }

        /// <summary>
        /// Gets the arguments provided by the command under test.
        /// </summary>
        public TArguments Arguments { get; private set; }

        /// <summary>
        /// Returns true if the Test method was called; false otherwise.
        /// </summary>
        public bool TestCalled { get; private set; }
    }
}

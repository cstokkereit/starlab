namespace StarLab.Commands
{
    /// <summary>
    /// A mock class used for testing the <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class.
    /// </summary>
    class MockArguments
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MockArguments"/> class.
        /// </summary>
        /// <param name="username">The username argument.</param>
        /// <param name="password">The password argument.</param>
        public MockArguments(string username, string password)
        {
            Password = password;
            UserName = username;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName { get; private set; }
    }
}

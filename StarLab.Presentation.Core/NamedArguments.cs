namespace StarLab.Presentation
{
    /// <summary>
    /// A collection of named arguments.
    /// </summary>
    public class NamedArguments : INamedArguments
    {
        private Dictionary<string, object> arguments = new Dictionary<string, object>(); // A dictionary containing the arguments indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="NamedArguments"/> class.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="argument">The argument being provided.</param>
        public NamedArguments(string name, object argument)
        {
            Add(name, argument);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="NamedArguments"/> class.
        /// </summary>
        public NamedArguments()
        {
            // Do Nothing
        }

        /// <summary>
        /// Adds the argument provided with the specified name.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="argument">The argument being added.</param>
        /// <returns>A reference to this instance allowing fluent addition of arguments.</returns>
        public INamedArguments Add(string name, object argument)
        {
            arguments.Add(name, argument);

            return this;
        }

        /// <summary>
        /// Gets the argument with the specified name.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="name">The name of the argument.</param>
        /// <returns>The argument with the specified name.</returns>
        public T GetArgument<T>(string name)
        {
            return (T)arguments[name];
        }
    }
}

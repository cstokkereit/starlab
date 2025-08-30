namespace Stratosoft.Commands
{
    /// <summary>
    /// A collection of name/value pairs that can be used as the arguments for the Execute(Arguments) method of a <see cref="Command&lt;Arguments&gt;"/>.
    /// </summary>
    public sealed class Arguments
    {
        private readonly Dictionary<string, object> arguments = new Dictionary<string, object>(); // A dictionary containing the name/value pairs.

        /// <summary>
        /// Gets the argument with the specified name.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <returns>The required argument value.</returns>
        public object this[string name]
        {
            get { return arguments[name]; }
        }

        /// <summary>
        /// Gets the number of arguments.
        /// </summary>
        public int Count { get { return arguments.Count; } }

        /// <summary>
        /// Gets a list containing the names of the arguments.
        /// </summary>
        public List<string> Names { get { return new List<string>(arguments.Keys); } }

        /// <summary>
        /// Adds an argument to the collection.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        public void Add(string name, object value)
        {
            arguments.Add(name, value);
        }
    }
}

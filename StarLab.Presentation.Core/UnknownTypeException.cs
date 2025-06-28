namespace StarLab.Presentation
{
    /// <summary>
    /// The exception that is thrown when the specified <see cref="Type."> does not exist.
    /// </summary>
    public class UnknownTypeException : Exception
    {
        private const string MESSAGE = "The specified type does not exist.";

        /// <summary>
        /// Initialises a new instance of the <see cref="UnknownTypeException"/> class.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        public UnknownTypeException(string typeName)
            : base(MESSAGE) 
        {
            TypeName = typeName;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string TypeName { get; }
    }
}

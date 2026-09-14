namespace StarLab.Application
{
    /// <summary>
    /// An exception that is thrown when an invalid name is provided.
    /// </summary>
    public class InvalidNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNameException"/> class.
        /// </summary>
        /// <param name="name">The invalid name.</param>
        public InvalidNameException(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }
    }
}

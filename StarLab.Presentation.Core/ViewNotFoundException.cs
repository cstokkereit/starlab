namespace StarLab.Presentation
{
    /// <summary>
    /// The exception that is thrown when an <see cref="IView"> with the specified ID could not be found.
    /// </summary>
    public class ViewNotFoundException : Exception
    {
        private const string MESSAGE = "The specified view could not be found.";

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The view ID.</param>
        public ViewNotFoundException(string id)
            : base(MESSAGE) 
        { 
            ID = id;
        }

        /// <summary>
        /// Gets the ID of the missing view.
        /// </summary>
        public string ID { get; }
    }
}

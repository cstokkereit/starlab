namespace StarLab.Presentation
{
    /// <summary>
    /// Provides context for the ActiveViewChanged event.
    /// </summary>
    public class ActiveViewChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ActiveViewChangedEventArgs"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that was activated.</param>
        public ActiveViewChangedEventArgs(IView view)
        {
            View = view;
        }

        /// <summary>
        /// Gets the <see cref="IView"/> that was activated.
        /// </summary>
        public IView View { get; }
    }
}

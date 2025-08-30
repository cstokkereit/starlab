namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties and methods that are common to all views.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the <see cref="IChildViewController"/> that controls the view.
        /// </summary>
        IViewController Controller { get; }

        /// <summary>
        /// Gets or sets a flag that determines whether the view will be hidden or unloaded when it is closed.
        /// </summary>
        bool HideOnClose { get; set; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets or sets the view name;
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        void Show(IView view);

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowOpenFileDialog(string title, string filter);

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        string ShowSaveFileDialog(string title, string filter, string extension);
    }
}

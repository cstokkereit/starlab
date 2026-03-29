namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDockableViewPresenter"/> to control the behaviour of a tool window.
    /// </summary>
    public interface IDockableView : IParentView
    {
        /// <summary>
        /// Gets or sets a flag that determines whether the view will be hidden or unloaded when it is closed.
        /// </summary>
        bool HideOnClose { get; set; }

        /// <summary>
        /// Gets the view text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();
    }
}

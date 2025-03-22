namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IApplicationViewPresenter"/> to control the behaviour of the workspace view.
    /// </summary>
    public interface IApplicationView : IView, IMenuManager, IToolbarManager
    {
        /// <summary>
        /// Closes the currently selected document.
        /// </summary>
        void CloseActiveDocument();

        /// <summary>
        /// Closes all documents.
        /// </summary>
        void CloseAll();

        /// <summary>
        /// Generates an XML representation of the workspace including the size, state and location of each of the dockable windows it contains.
        /// </summary>
        /// <returns>An XML representation of the workspace.</returns>
        string GetLayout();

        /// <summary>
        /// Uses the layout provided to set the size, state and location of each of the dockable windows within the workspace.
        /// </summary>
        /// <param name="layout">An XML representation of the workspace.</param>
        void SetLayout(string layout);
    }
}

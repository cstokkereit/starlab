namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDockableViewPresenter"/> to control the behaviour of a document window.
    /// </summary>
    public interface IDocumentView : IDockableView, IToolbarManager
    {
        /// <summary>
        /// Hides the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be hidden.</param>
        void HideSplitContent(string name);

        /// <summary>
        /// Shows the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be shown.</param>
        void ShowSplitContent(string name);
    }
}

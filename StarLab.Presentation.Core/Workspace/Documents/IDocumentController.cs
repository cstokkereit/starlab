namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Represents a controller that can be used to control a document window.
    /// </summary>
    public interface IDocumentController : IViewController, IToolbarManager
    {
        /// <summary>
        /// Closes the document window.
        /// </summary>
        void Close();

        /// <summary>
        /// Gets the specified <see cref="IChildViewController"/> 
        /// </summary>
        /// <param name="name">The name of the required controller.</param>
        /// <returns>The required <see cref="IChildViewController"/>.</returns>
        IChildViewController GetController(string name);

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

        /// <summary>
        /// Updates the <see cref="IDocument"/> that the document window represents.
        /// </summary>
        /// <param name="document">The new <see cref="IDocument"/>.</param>
        void UpdateDocument(IDocument document);
    }
}

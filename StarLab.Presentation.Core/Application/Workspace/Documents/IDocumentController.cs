namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents a controller that can be used to control a document window.
    /// </summary>
    public interface IDocumentController : IViewController, IToolbarManager
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

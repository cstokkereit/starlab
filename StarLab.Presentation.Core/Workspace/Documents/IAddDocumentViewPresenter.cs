namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Defines the methods used by the <see cref="IAddDocumentView"/> to communicate with its presenter.
    /// </summary>
    public interface IAddDocumentViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// Adds the selected document to the workspace.
        /// </summary>
        /// <param name="name">The name of the document.</param>
        /// <param name="definitionName">The name of the document definition.</param>
        void AddDocument(string name, string definitionName);
    }
}

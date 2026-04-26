using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the add document functionality.
    /// </summary>
    public interface IAddDocumentUseCaseService : IUseCaseService
    {
        /// <summary>
        /// Adds a document to the workspace.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="document">A <see cref="DocumentDTO"/> that holds the details of the document to be added.</param>
        void AddDocument(IWorkspace workspace, DocumentDTO document);
    }
}

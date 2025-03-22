using StarLab.Application.Workspace.Documents;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Provides context for the <see cref="IAddDocumentUseCase"/>.
    /// </summary>
    public readonly struct AddDocumentInteractionContext : IInteractionContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractionContext"> class.
        /// </summary>
        /// <param name="workspace">The currently open <see cref="IWorkspace"/>.</param>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        /// <param name="type">An <see cref="DocumentType"/> that specifies the type of document to add.</param>
        public AddDocumentInteractionContext(IWorkspace workspace, string path, DocumentType type)
        {
            Workspace = workspace;
            Path = path;
            Type = type;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractionContext"> class.
        /// </summary>
        /// <param name="workspace">The currently open <see cref="IWorkspace"/>.</param>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        public AddDocumentInteractionContext(IWorkspace workspace, string path)
        {
            Workspace = workspace;
            Path = path;
        }

        /// <summary>
        /// Gets the path to  the folder.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the <see cref="DocumentType"/>.
        /// </summary>
        public DocumentType Type { get; }

        /// <summary>
        /// Gets the <see cref="IWorkspace"/>.
        /// </summary>
        public IWorkspace Workspace { get; }
    }
}

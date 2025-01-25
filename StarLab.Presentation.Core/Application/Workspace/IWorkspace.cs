using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents the workspace.
    /// </summary>
    public interface IWorkspace
    {
        /// <summary>
        /// Gets the active <see cref="IDocument"/>.
        /// </summary>
        IDocument? ActiveDocument { get; }

        /// <summary>
        /// Gets the documents within the workspace.
        /// </summary>
        IEnumerable<IDocument> Documents { get; }

        /// <summary>
        /// Gets the workspace file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the folders within the workspace.
        /// </summary>
        IEnumerable<IFolder> Folders { get; }

        /// <summary>
        /// Gets the workspace layout.
        /// </summary>
        string Layout { get; }

        /// <summary>
        /// Gets the workspace name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the projects within the workspace.
        /// </summary>
        IEnumerable<IProject> Projects { get; }

        /// <summary>
        /// Clears the active document.
        /// </summary>
        void ClearActiveDocument();

        /// <summary>
        /// Collapses all of the projects and folders within the workspace.
        /// </summary>
        void Collapse();

        /// <summary>
        /// Expands all of the projects and folders within the workspace.
        /// </summary>
        void Expand();

        /// <summary>
        /// Gets the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDocument"/>.</param>
        /// <returns>The <see cref="IDocument"/> with the specified ID.</returns>
        IDocument GetDocument(string id);

        /// <summary>
        /// Gets the <see cref="IFolder"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IFolder"/>.</param>
        /// <returns>The <see cref="IFolder"/> with the specified key.</returns>
        IFolder GetFolder(string key);

        /// <summary>
        /// Gets the <see cref="IProject"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IProject"/>.</param>
        /// <returns>The <see cref="IProject"/> with the specified key.</returns>
        IProject GetProject(string key);

        /// <summary>
        /// Determines if the workspace contains the specified document.
        /// </summary>
        /// <param name="id">The ID of the required document.</param>
        /// <returns><see cref="true"/> if the workspace contains a document with the specified ID; <see cref="false"/> otherwise.</returns>
        bool HasDocument(string id);

        /// <summary>
        /// Determines if the workspace contains the specified folder.
        /// </summary>
        /// <param name="key">The key of the required folder.</param>
        /// <returns><see cref="true"/> if the workspace contains a folder with the specified key; <see cref="false"/> otherwise.</returns>
        bool HasFolder(string key);

        /// <summary>
        /// Determines if the workspace contains the specified project.
        /// </summary>
        /// <param name="key">The key of the required project.</param>
        /// <returns><see cref="true"/> if the workspace contains a project with the specified key; <see cref="false"/> otherwise.</returns>
        bool HasProject(string key);

        /// <summary>
        /// Sets the active document to be the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the active <see cref="IDocument"/>.</param>
        void SetActiveDocument(string id);

        /// <summary>
        /// Updates the workspace layout.
        /// </summary>
        /// <param name="layout">The new layout.</param>
        void UpdateLayout(string layout);
    }
}

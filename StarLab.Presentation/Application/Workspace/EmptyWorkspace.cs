using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents an empty workspace.
    /// </summary>
    internal class EmptyWorkspace : IWorkspace
    {
        /// <summary>
        /// Gets the active <see cref="IDocument"/>.
        /// </summary>
        public IDocument? ActiveDocument => null;

        /// <summary>
        /// Gets the documents within the workspace.
        /// </summary>
        public IEnumerable<IDocument> Documents => new List<IDocument>();

        /// <summary>
        /// Gets the workspace file name.
        /// </summary>
        public string FileName => string.Empty;

        /// <summary>
        /// Gets the folders within the workspace.
        /// </summary>
        public IEnumerable<IFolder> Folders => new List<IFolder>();

        /// <summary>
        /// Gets the workspace layout.
        /// </summary>
        public string Layout => string.Empty;

        /// <summary>
        /// Gets the workspace name.
        /// </summary>
        public string Name => string.Empty;

        /// <summary>
        /// Gets the projects within the workspace.
        /// </summary>
        public IEnumerable<IProject> Projects => new List<IProject>();

        /// <summary>
        /// Clears the active document.
        /// </summary>
        public void ClearActiveDocument()
        {
            // Do Nothing
        }

        /// <summary>
        /// Collapses all of the projects and folders within the workspace.
        /// </summary>
        public void Collapse()
        {
            // Do Nothing
        }

        /// <summary>
        /// Expands all of the projects and folders within the workspace.
        /// </summary>
        public void Expand()
        {
            // Do Nothing
        }

        /// <summary>
        /// Gets the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDocument"/>.</param>
        /// <returns>The <see cref="IDocument"/> with the specified ID.</returns>
        public IDocument GetDocument(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="IFolder"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IFolder"/>.</param>
        /// <returns>The <see cref="IFolder"/> with the specified key.</returns>
        public IFolder GetFolder(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="IProject"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IProject"/>.</param>
        /// <returns>The <see cref="IProject"/> with the specified key.</returns>
        public IProject GetProject(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the workspace contains the specified document.
        /// </summary>
        /// <param name="id">The ID of the required document.</param>
        /// <returns>true if the workspace contains a document with the specified ID; false otherwise.</returns>
        public bool HasDocument(string id)
        {
            return false;
        }

        /// <summary>
        /// Determines if the workspace contains the specified folder.
        /// </summary>
        /// <param name="key">The key of the required folder.</param>
        /// <returns>true if the workspace contains a folder with the specified key; false otherwise.</returns>
        public bool HasFolder(string key)
        {
            return false;
        }

        /// <summary>
        /// Determines if the workspace contains the specified project.
        /// </summary>
        /// <param name="key">The key of the required project.</param>
        /// <returns>true if the workspace contains a project with the specified key; false otherwise.</returns>
        public bool HasProject(string key)
        {
            return false;
        }

        /// <summary>
        /// Sets the active document to be the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the active <see cref="IDocument"/>.</param>
        public void SetActiveDocument(string id)
        {
            // Do Nothing
        }

        /// <summary>
        /// Updates the workspace layout.
        /// </summary>
        /// <param name="layout">The new layout.</param>
        public void UpdateLayout(string layout)
        {
            // Do Nothing
        }
    }
}

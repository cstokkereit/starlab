using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a project within the workspace.
    /// </summary>
    public interface IProject : ICollapsible
    {
        /// <summary>
        /// Gets the documents in the project.
        /// </summary>
        IEnumerable<IDocument> Documents { get; }

        /// <summary>
        /// Gets the folders in the project.
        /// </summary>
        IEnumerable<IFolder> Folders { get; }

        /// <summary>
        /// Gets the project key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the project parent key.
        /// </summary>
        string ParentKey { get; }
    }
}

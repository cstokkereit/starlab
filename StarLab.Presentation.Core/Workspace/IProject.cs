using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Represents a project within the workspace.
    /// </summary>
    public interface IProject : IFolder
    {
        /// <summary>
        /// Gets the documents in the project.
        /// </summary>
        IEnumerable<IDocument> Documents { get; }

        /// <summary>
        /// Gets the folders in the project.
        /// </summary>
        IEnumerable<IFolder> Folders { get; }
    }
}

using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents a project in the workspace hierarchy.
    /// </summary>
    public class ProjectDTO
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProjectDTO"/> class.
        /// </summary>
        public ProjectDTO()
        {
            Documents = new List<DocumentDTO>();
            Folders = new List<FolderDTO>();
        }

        public List<DocumentDTO> Documents;

        public bool Expanded;

        public List<FolderDTO> Folders;

        public string? Name;
    }
}

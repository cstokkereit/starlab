using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents a project in the workspace hierarchy.
    /// </summary>
    public class ProjectDTO
    {
        public DatabaseDTO Database = new DatabaseDTO();

        public List<DocumentDTO> Documents = new List<DocumentDTO>();

        public bool Expanded;

        public List<FolderDTO> Folders = new List<FolderDTO>();

        public string Name = string.Empty;
    }
}

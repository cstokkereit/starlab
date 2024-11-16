using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public class ProjectDTO
    {
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

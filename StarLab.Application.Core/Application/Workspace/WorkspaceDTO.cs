using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public class WorkspaceDTO
    {
        public WorkspaceDTO()
        {
            Documents = new List<DocumentDTO>();
            Folders = new List<FolderDTO>();
        }

        public List<DocumentDTO> Documents;

        public string? FileName;

        public List<FolderDTO> Folders;

        public string? Layout;
    }
}

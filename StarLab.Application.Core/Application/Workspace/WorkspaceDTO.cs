namespace StarLab.Application.Workspace
{
    public class WorkspaceDTO
    {
        public WorkspaceDTO()
        {
            Projects = new List<ProjectDTO>();
        }

        public string? ActiveDocument;

        public string? FileName;

        public List<ProjectDTO> Projects;

        public string? Layout;
    }
}
